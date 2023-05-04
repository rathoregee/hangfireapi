using g2v.core.clinetsync.common.Classes.Results;
using Xunit;

namespace g2v.core.clinetsync.unittests.Results
{
    public class ClientResultTest
    {
        [Fact]
        public void Accepted()
        {
            Assert.Equal(ClientResultStatus.Accepted, ClientResult.Accepted().Status);
        }

        [Fact]
        public void Accepted_T()
        {
            Assert.Equal(ClientResultStatus.Accepted, ClientResult.Accepted<NullPayload>().Status);
        }

        [Fact]
        public void Success()
        {
            Assert.Equal(ClientResultStatus.Success, ClientResult.Success().Status);
        }

        [Fact]
        public void Success_T()
        {
            Assert.Equal(ClientResultStatus.Success, ClientResult.Success<NullPayload>().Status);
        }

        [Fact]
        public void Created()
        {
            Assert.Equal(ClientResultStatus.Created, ClientResult.Created().Status);
        }

        [Fact]
        public void Created_T()
        {
            Assert.Equal(ClientResultStatus.Created, ClientResult.Created<NullPayload>().Status);
        }

        [Fact]
        public void Updated()
        {
            Assert.Equal(ClientResultStatus.Updated, ClientResult.Updated().Status);
        }

        [Fact]
        public void Updated_T()
        {
            Assert.Equal(ClientResultStatus.Updated, ClientResult.Updated<NullPayload>().Status);
        }

        [Fact]
        public void Deleted()
        {
            Assert.Equal(ClientResultStatus.Deleted, ClientResult.Deleted().Status);
        }

        [Fact]
        public void Deleted_T()
        {
            Assert.Equal(ClientResultStatus.Deleted, ClientResult.Deleted<NullPayload>().Status);
        }

        [Fact]
        public void ValidationError()
        {
            Assert.Equal(ClientResultStatus.ValidationError, ClientResult.ValidationError().Status);
        }

        [Fact]
        public void ValidationError_T()
        {
            Assert.Equal(ClientResultStatus.ValidationError, ClientResult.ValidationError<NullPayload>().Status);
        }

        [Fact]
        public void NotFound()
        {
            Assert.Equal(ClientResultStatus.NotFound, ClientResult.NotFound().Status);
        }

        [Fact]
        public void NotFound_T()
        {
            Assert.Equal(ClientResultStatus.NotFound, ClientResult.NotFound<NullPayload>().Status);
        }

        [Fact]
        public void Forbidden()
        {
            Assert.Equal(ClientResultStatus.Forbidden, ClientResult.Forbidden().Status);
        }

        [Fact]
        public void Forbidden_T()
        {
            Assert.Equal(ClientResultStatus.Forbidden, ClientResult.Forbidden<NullPayload>().Status);
        }

        [Fact]
        public void ServiceUnavailable()
        {
            Assert.Equal(ClientResultStatus.ServiceUnavailable, ClientResult.ServiceUnavailable().Status);
        }

        [Fact]
        public void ServiceUnavailable_T()
        {
            Assert.Equal(ClientResultStatus.ServiceUnavailable, ClientResult.ServiceUnavailable<NullPayload>().Status);
        }

        [Fact]
        public void Conflict()
        {
            Assert.Equal(ClientResultStatus.Conflict, ClientResult.Conflict().Status);
        }

        [Fact]
        public void Conflict_T()
        {
            Assert.Equal(ClientResultStatus.Conflict, ClientResult.Conflict<NullPayload>().Status);
        }

        [Fact]
        public void Unauthorized()
        {
            Assert.Equal(ClientResultStatus.Unauthorized, ClientResult.Unauthorized().Status);
        }

        [Fact]
        public void Unauthorized_T()
        {
            Assert.Equal(ClientResultStatus.Unauthorized, ClientResult.Unauthorized<NullPayload>().Status);
        }
    }
}