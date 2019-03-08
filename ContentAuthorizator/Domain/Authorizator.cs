namespace ContentAuthorizator.Domain
{
    public class Authorizator : IAuthorizator
    {
        private IAuthorizationRepository auths;
        public Authorizator(IAuthorizationRepository auths)
        {
            this.auths = auths;
        }

        public bool IsAuthorizationValid(IAuthorization auth)
        {
            var storedAuth = auths.Retrieve(auth.IPAdress);

            if(storedAuth == null)
                return false;

            return storedAuth == auth;
        }
    }
}
