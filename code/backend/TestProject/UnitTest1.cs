using FakeMyResume.DTOs;
//using Newtonsoft.Json;
using System.Text.Json;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            CreateResumeDTO createResumeDTO = new CreateResumeDTO();
            String j = JsonSerializer.Serialize(createResumeDTO);
            Assert.Equal(2, 2);
        }
    }
}