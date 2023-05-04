﻿using g2v.core.clinetsync.common.Interfaces.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.common.Classes.Results
{
    public static class ClientResult
    {
        private class ClientResultInternal<T> : IClientResult<T>
        {
            public string Status { get; }
            public string[] Errors { get; }

            private readonly T _payload;

            public T Payload => _payload;

            public object PayloadAsObject => _payload;

            private ClientResultInternal(string status)
            {
                Status = status;
            }

            private ClientResultInternal(string status, T payload)
                : this(status)
            {
                _payload = payload;
            }

            private ClientResultInternal(string status, string[] errors)
                : this(status)
            {
                Errors = errors;
            }
            public static IClientResult<T> AcceptedInternal(T payload)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Accepted, payload);
            }

            public static IClientResult<T> SuccessInternal(T payload)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Success, payload);
            }

            public static IClientResult<T> CreatedInternal(T payload)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Created, payload);
            }

            public static IClientResult<T> UpdatedInternal(T payload)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Updated, payload);
            }

            public static IClientResult<T> DeletedInternal(T payload)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Deleted, payload);
            }

            public static IClientResult<T> NotFoundInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.NotFound, errors);
            }

            public static IClientResult<T> ValidationErrorInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.ValidationError, errors);
            }

            public static IClientResult<T> ServiceUnavailableInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.ServiceUnavailable, errors);
            }

            public static IClientResult<T> ConflictInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Conflict, errors);
            }

            public static IClientResult<T> ForbiddenInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Forbidden, errors);
            }

            public static IClientResult<T> UnauthorizedInternal(params string[] errors)
            {
                return new ClientResultInternal<T>(ClientResultStatus.Unauthorized, errors);
            }
        }

        public static IClientResult Accepted()
        {
            return Accepted(new NullPayload());
        }

        public static IClientResult<T> Accepted<T>()
        {
            return ClientResultInternal<T>.AcceptedInternal(default);
        }

        public static IClientResult<T> Accepted<T>(T payload)
        {
            return ClientResultInternal<T>.AcceptedInternal(payload);
        }

        public static IClientResult Success()
        {
            return Success(new NullPayload());
        }

        public static IClientResult<T> Success<T>()
        {
            return ClientResultInternal<T>.SuccessInternal(default);
        }

        public static IClientResult<T> Success<T>(T payload)
        {
            return ClientResultInternal<T>.SuccessInternal(payload);
        }

        public static IClientResult Created()
        {
            return Created(new NullPayload());
        }

        public static IClientResult<T> Created<T>()
        {
            return ClientResultInternal<T>.CreatedInternal(default);
        }

        public static IClientResult<T> Created<T>(T payload)
        {
            return ClientResultInternal<T>.CreatedInternal(payload);
        }

        public static IClientResult Updated()
        {
            return Updated(new NullPayload());
        }

        public static IClientResult<T> Updated<T>()
        {
            return ClientResultInternal<T>.UpdatedInternal(default);
        }

        public static IClientResult<T> Updated<T>(T payload)
        {
            return ClientResultInternal<T>.UpdatedInternal(payload);
        }

        public static IClientResult Deleted()
        {
            return Deleted(new NullPayload());
        }

        public static IClientResult<T> Deleted<T>()
        {
            return ClientResultInternal<T>.DeletedInternal(default);
        }

        public static IClientResult<T> Deleted<T>(T payload)
        {
            return ClientResultInternal<T>.DeletedInternal(payload);
        }

        public static IClientResult NotFound(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.NotFoundInternal(errors);
        }

        public static IClientResult<T> NotFound<T>(params string[] errors)
        {
            return ClientResultInternal<T>.NotFoundInternal(errors);
        }

        public static IClientResult ServiceUnavailable(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.ServiceUnavailableInternal(errors);
        }

        public static IClientResult<T> ServiceUnavailable<T>(params string[] errors)
        {
            return ClientResultInternal<T>.ServiceUnavailableInternal(errors);
        }

        public static IClientResult ValidationError(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.ValidationErrorInternal(errors);
        }

        public static IClientResult<T> ValidationError<T>(params string[] errors)
        {
            return ClientResultInternal<T>.ValidationErrorInternal(errors);
        }

        public static IClientResult Conflict(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.ConflictInternal(errors);
        }

        public static IClientResult<T> Conflict<T>(params string[] errors)
        {
            return ClientResultInternal<T>.ConflictInternal(errors);
        }

        public static IClientResult Forbidden(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.ForbiddenInternal(errors);
        }

        public static IClientResult<T> Forbidden<T>(params string[] errors)
        {
            return ClientResultInternal<T>.ForbiddenInternal(errors);
        }

        public static IClientResult Unauthorized(params string[] errors)
        {
            return ClientResultInternal<NullPayload>.UnauthorizedInternal(errors);
        }

        public static IClientResult<T> Unauthorized<T>(params string[] errors)
        {
            return ClientResultInternal<T>.UnauthorizedInternal(errors);
        }
    }
}
