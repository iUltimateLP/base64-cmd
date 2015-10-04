using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace base64_encode_decode
{
    class Program
    {
        public enum Instructions
        {
            Encode,
            Decode,
            NotSure
        }

        static void Main(string[] args)
        {
            new Program().Start(args);
        }

        public void Start(string[] args)
        {
            //b64.exe <encode/decode> -i=path/to/encoded/file -o=path/to/decoded/file (or other way around)

            char[] delimiters = { '=' };
            Instructions instr = Instructions.NotSure;

            printStart();

            if (args.Length != 3)
            {
                doErrorMessage(2);
            }
            else
            {
                string instruction = args[0];
                string arg2 = args[1];
                string arg3 = args[2];

                string input_file = "";
                string output_file = "";

                if (instruction.ToLower() != "encode" && instruction.ToLower() != "decode")
                    doErrorMessage(1);

                if (!arg2.StartsWith("-i=") && !arg2.StartsWith("-o="))
                    doErrorMessage(2);

                if (!arg3.StartsWith("-i=") && !arg3.StartsWith("-o="))
                    doErrorMessage(2);

                if (instruction.ToLower() == "encode")
                    instr = Instructions.Encode;
                else if (instruction.ToLower() == "decode")
                    instr = Instructions.Decode;
                
                if (arg2.StartsWith("-i="))
                {
                    string[] split = arg2.Split(delimiters);
                    input_file = split[1];
                }

                if (arg3.StartsWith("-i="))
                {
                    string[] split = arg3.Split(delimiters);
                    input_file = split[1];
                }

                if (arg2.StartsWith("-o="))
                {
                    string[] split = arg2.Split(delimiters);
                    output_file = split[1];
                }

                if (arg3.StartsWith("-o="))
                {
                    string[] split = arg3.Split(delimiters);
                    output_file = split[1];
                }

                doConverting(input_file, output_file, instr);
            }
        }

        public void printStart()
        {
            Console.WriteLine("=== Base64 encode/decode tool written by Johnny / iUltimateLP @ GitHub ===");
            Console.WriteLine("=== ");
            Console.WriteLine("=== Usage: b64.exe encode/decode [options]");
            Console.WriteLine("=== Options: -i=path/to/file -- Input file");
            Console.WriteLine("===          -o=path/to/file -- Output file");
            Console.WriteLine("===");
            Console.WriteLine("==========================================================================\n");
        }

        public void doErrorMessage(int id)
        {
            string errmsg = "";
            switch (id)
            {
                case 1:
                    errmsg = "Wrong method. Available methods are 'encode' and 'decode'";
                    break;
                case 2:
                    errmsg = "Wrong count of arguments. Use encode/decode, -i and -o.";
                    break;
                case 3:
                    errmsg = "The input file does not exist";
                    break;
                default:
                    break;
            }

            Console.WriteLine("[ERROR] " + errmsg);
            System.Environment.Exit(0);
        }

        public void doConverting(string inputdir, string outputdir, Instructions instr)
        {
            if (!File.Exists(inputdir))
            {
                doErrorMessage(3);
            }

            Stopwatch sw = Stopwatch.StartNew();

            Console.WriteLine("Starting conversion of " + inputdir);

            string input_content = File.ReadAllText(inputdir);
            string output_content = "";

            switch (instr)
            {
                case Instructions.Encode:
                    output_content = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input_content));
                    break;
                case Instructions.Decode:
                    output_content = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input_content));
                    break;
                case Instructions.NotSure:
                    break;
                default:
                    break;
            }

            Console.WriteLine("Conversion done.");
            Console.WriteLine("Writing to file...");
            File.WriteAllText(outputdir, output_content);

            Console.WriteLine("Conversion ended in " + (sw.ElapsedMilliseconds+5) + "ms");

            sw.Stop();
        }
    }
}
