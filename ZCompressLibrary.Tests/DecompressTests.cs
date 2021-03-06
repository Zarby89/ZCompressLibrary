﻿using System;
using System.Linq;
using Xunit;
using System.IO;
using ZCompressLibrary;
using Xunit.Abstractions;

namespace ZCompressLibrary.Tests
{
    public class DecompressTests
    {
        readonly ITestOutputHelper output;

        public DecompressTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_decompress_file_moldorm_bin()
        {
            var file = File.ReadAllBytes("moldorm.bin");
            int compsize = 0;
            var decompressed = Decompress.ALTTPDecompressGraphics(file, 0, file.Length, ref compsize);
        }

        [Fact]
        public void should_decompress_feesh_mode_rom()
        {
            var file = File.ReadAllBytes("Feesh_Mode_Patched.sfc");
            int compsize = 0;
            var decompressed = Decompress.ALTTPDecompressGraphics(file, 0x08B800, file.Length - 0x08B800, ref compsize);
        }

        [Fact]
        public void should_decompress_file_ganon1_bin_and_be_same_as_ganon1_gfx()
        {
            var file = File.ReadAllBytes("ganon1.bin");
            var expected = File.ReadAllBytes("ganontest.gfx");
            int compsize = 0;
            var decompressed = Decompress.ALTTPDecompressGraphics(file, 0, file.Length, ref compsize);
            int i = 0;
            while(i < expected.Length && i < decompressed.Length)
            {
                if(expected[i] != decompressed[i])
                {
                    output.WriteLine($"mismatch at {i}, expected: {expected[i].ToString("X2")} decompressed: {decompressed[i].ToString("X2")}");
                }
                i++;
            }
            Assert.Equal(expected, decompressed);
        }
    }
}
