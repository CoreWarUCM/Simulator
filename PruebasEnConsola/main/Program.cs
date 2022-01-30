using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace main
{
    class MyStream : Stream
    {
        private bool first = true;
        private int doubleCount = 0;

        private int MAX_STEPS = 80000;
        
        public override void Flush() {}

        public override int Read(byte[] buffer, int offset, int count)
        {
            buffer[0] = (byte) ((doubleCount == MAX_STEPS -2) ? 'c' : 's');
            buffer[1] = (byte) ('\n');
            return ++doubleCount > MAX_STEPS ? 0:2;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return doubleCount;
        }

        public override void SetLength(long value)
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = false;
        public override bool CanWrite { get; } = false;
        public override long Length { get; } = 0;
        public override long Position { get; set; }
    }
    
    class Program
    {
        static async Task  Main(string[] args)
        {
            string pathToWarrior1 = "./dwarf.redcode";
            string pathToWarrior2 = "./imp.redcode";
            string pMarsPath = "./pMars.exe";
            var stdOutBuffer = new StringBuilder();

            var cmd = Cli.Wrap(pMarsPath).WithArguments($"-e -P {pathToWarrior1} {pathToWarrior2}")
                .WithStandardInputPipe(PipeSource.FromStream(new MyStream()))
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer));
            var res = await cmd.ExecuteBufferedAsync();
            Console.WriteLine(stdOutBuffer);

        }
    }
}