import React from 'react'
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'

const DemoChart = (props) => (
    <div> <HighchartsReact     highcharts={Highcharts}     options={props.options}   /> </div>
);

export default DemoChart;