using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace AdventOfCode21
{
    internal class Day_16 : BaseDay
    {
        private string input;

        public Day_16()
        {
            input = File.ReadAllText(InputFilePath);
        }

        private Dictionary<char, bool[]> bitConverter = new()
        {
            ['0'] = new bool[] { false, false, false, false },
            ['1'] = new bool[] { false, false, false, true },
            ['2'] = new bool[] { false, false, true, false },
            ['3'] = new bool[] { false, false, true, true },
            ['4'] = new bool[] { false, true, false, false },
            ['5'] = new bool[] { false, true, false, true },
            ['6'] = new bool[] { false, true, true, false },
            ['7'] = new bool[] { false, true, true, true },
            ['8'] = new bool[] { true, false, false, false },
            ['9'] = new bool[] { true, false, false, true },
            ['A'] = new bool[] { true, false, true, false },
            ['B'] = new bool[] { true, false, true, true },
            ['C'] = new bool[] { true, true, false, false },
            ['D'] = new bool[] { true, true, false, true },
            ['E'] = new bool[] { true, true, true, false },
            ['F'] = new bool[] { true, true, true, true },
        };

        public int BitToInt(ArraySegment<bool> bits)
        {
            int num = 0;
            for (int i = bits.Count - 1; i >= 0; i--)
            {
                if (bits[i])
                    num += (int)Math.Pow(2, i);
            }
            return num;
        }

        public override ValueTask<string> Solve_1()
        {
            Console.WriteLine(input);
            bool[] bits = new bool[0];
            foreach (char c in input)
            {
                bits = bits.Concat(bitConverter[c]).ToArray();
            }
            int i = 0;

            BitSequence sequence = new(bits);

            while (i < bits.Count())
            {
                ArraySegment<bool> _packetVersion = new(bits, i, 3);
                var packetVersion = BitToInt(_packetVersion);
                i += 3;
                ArraySegment<bool> _packetType = new(bits, i, 3);
                var packetType = BitToInt(_packetType);
                i += 3;

                BitSequence seq = new(bits);

                if (packetType == 4) //literal value
                {
                    ParseLiteral(seq);
                }
                else // operator packet
                {
                    bool lengthTypeId = bits[i];
                    if (lengthTypeId)
                    {
                        ArraySegment<bool> _totalSubpacketLength = new(bits, i, 15);
                        int totalSubpacketLength = BitToInt(_totalSubpacketLength);
                        i += 15;
                    }
                    else
                    {
                        ArraySegment<bool> _numOfSubPackets = new(bits, i, 11);
                        int numOfSubPackets = BitToInt(_numOfSubPackets);
                        i += 11;
                    }
                }
            }

            return new("aha");
        }

        public int ParseLiteral(BitSequence seq)
        {
            List<int> digits = new();
            while (true)
            {
                bool hasNextGroup = seq.Current;
                seq.MoveOne();
                ArraySegment<bool> nextDigit = seq.GetNextSegment(4);
                digits.Add(BitToInt(nextDigit));
                if (!hasNextGroup)
                {
                    seq.SkipZeroBits();
                    break;
                }
            }
            string literal = "";
            foreach (int digit in digits)
            {
                literal = literal + digit.ToString();
            }
            return int.Parse(literal);
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        public class BitSequence
        {
            public readonly bool[] bits;
            public int index;

            public bool Current => bits[index];

            public BitSequence(bool[] bits)
            {
                this.bits = bits;
                index = 0;
            }

            public void MoveOne()
            {
                index++;
            }

            public void SkipZeroBits()
            {
                while (!bits[index])
                {
                    index++;
                }
            }

            public ArraySegment<bool> GetNextSegment(int segmentLength)
            {
                var segment = new ArraySegment<bool>(bits, index, segmentLength);
                index += segmentLength;
                return segment;
            }
        }
    }
}
