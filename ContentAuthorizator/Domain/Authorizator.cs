namespace ContentAuthorizator.Domain
{
    public class Authorizator : IAuthorizator
    {
        public IAuthorizationRepository Auths { get; set; }
        public Authorizator(IAuthorizationRepository auths)
        {
            this.Auths = auths;
        }

        public bool IsAuthorizationValid(IAuthorization auth)
        {
            var storedAuth = Auths.Retrieve(auth.IPAdress);

            if(storedAuth == null)
                return false;

            var result = storedAuth.Equals(auth);
            return result;
        }
    }
}
