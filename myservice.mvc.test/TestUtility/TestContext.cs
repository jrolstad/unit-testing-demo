using System;
using System.Collections.Generic;

namespace myservice.mvc.test.TestUtility
{
    public class TestContext
    {
        public string InMemoryDatabaseIdentitifer = Guid.NewGuid().ToString();
        public Dictionary<string,string> ConfigurationValues = new Dictionary<string, string>();
        public Dictionary<string,string> EmailAddreses = new Dictionary<string, string>();
    }
}