using System;

class Server
{
    public void Start()
    {
        new TestCommsObject().TestLog();
        Console.WriteLine("Server started");
    }

    class TestCommsObject : GSFCommsObject
    {
        public void TestLog()
        {
            Log("Test Log");
            LogError("test");
        }
    }
}
