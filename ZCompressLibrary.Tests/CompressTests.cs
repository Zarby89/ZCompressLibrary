﻿using System;
using System.Linq;
using Xunit;
using System.IO;
using ZCompressLibrary;
using Xunit.Abstractions;

namespace ZCompressLibrary.Tests
{
    public class CompressTests
    {
        readonly ITestOutputHelper output;

        public CompressTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_compress_array()
        {
            Random r = new Random();
            var bytes = new byte[50];
            r.NextBytes(bytes);

            int compsize = 0;
            var compressed = Compress.ALTTPCompressGraphics(bytes, 0, bytes.Length, ref compsize);
            Assert.NotNull(compressed);
        }

        [Fact]
        public void should_decompress_file_moldorm_bin_and_recompress_and_decompress_to_same()
        {
            var file = File.ReadAllBytes("moldorm.bin");

            var decompressed = Decompress.ALTTPDecompressGraphics(file, 0, file.Length);
            File.WriteAllBytes("moldormdecomp1.bin", decompressed);

            int compsize = 0;
            var compressed = Compress.ALTTPCompressGraphics(decompressed, 0, decompressed.Length, ref compsize);
            Assert.NotNull(compressed);
            File.WriteAllBytes("moldormrecomp.bin", compressed);
            var decomp2 = Decompress.ALTTPDecompressGraphics(compressed, 0, compressed.Length);
            Assert.Equal(decompressed, decomp2);
        }

    }
}
