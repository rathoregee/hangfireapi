import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

import ChatWindow from './ChatWindow/ChatWindow';
import ChatInput from './ChatInput/ChatInput';

const Chat = () => {
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);

    latestChat.current = chat;

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:44306/chatHub')
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(result => {
                console.log('Connected!');
                connection.invoke('sendmessage', "kamran","billo");
                connection.on('ReceiveMessage', message => {
                    console.log(message)
                    const updatedChat = [...latestChat.current];
                    updatedChat.push(message);                
                    setChat(updatedChat);
                });
            })
            .catch(e => console.log('Connection failed: ', e));
    }, []);

    const sendMessage = async (user, message) => {
        const chatMessage = {
            user: user,
            message: message
        };

        try {
            await  fetch('https://localhost:5001/chat/messages', { 
                method: 'POST', 
                body: JSON.stringify(chatMessage),
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }
        catch(e) {
            console.log('Sending message failed.', e);
        }
    }

    return (
        <div>
            {/* <ChatInput sendMessage={sendMessage} />
            <hr /> */}
            <ChatWindow chat={chat}/>
        </div>
    );
};

export default Chat;
