using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.BL.Extensions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace DevActivator.Meetups.Tests.ProviderTests
{
    public class FriendProviderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public FriendProviderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TalkSpeakerIdsDeserializationSucceed()
        {
            // prepare
            var settings = new Settings {AuditRepoDirectory = "/Users/alex-mbp/repos/Audit"};
            var filePaths = settings.GetAllFilePaths("friends", false);

            // test
            var dic = new Dictionary<Encoding, int>();
            foreach (var filePath in filePaths)
            {
                var encoding = GetEncoding(filePath);
                if (!dic.ContainsKey(encoding))
                {
                    _testOutputHelper.WriteLine($"{nameof(encoding)}: {encoding}; {nameof(filePath)}: {filePath};");
                    dic.Add(encoding, 1);
                }
                else
                {
                    ++dic[encoding];
                }
            }

            foreach (var i in dic)
            {
                _testOutputHelper.WriteLine($"{nameof(i.Key)}: {i.Key}; {nameof(i.Value)}: {i.Value};");
            }

            Assert.Single(dic.Keys);
        }

        private static Encoding GetEncoding(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using (var reader = new StreamReader(filename, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) // you need this!
                    reader.Read();

                return reader.CurrentEncoding;
            }
        }
    }
}