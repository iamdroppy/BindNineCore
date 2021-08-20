namespace BindNineCore.Exceptions
{
    public class InvalidAliasRecordValueException : BindNineCoreException
    {
        public string GivenIp { get; }

        public InvalidAliasRecordValueException(string givenIp) : base(
            $"The given IP Address is not valid for an ALIAS record: {givenIp}")
        {
            GivenIp = givenIp;
        }
    }
}