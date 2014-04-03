using System.IO;

namespace TwainablePlus
{
    internal static class StreamExtensions
    {
        internal static ushort ReadUInt16(this Stream stream)
        {
            int byte1 = stream.ReadByte();

            if (byte1 == -1)
            {
                throw new EndOfStreamException();
            }

            int byte2 = stream.ReadByte();

            if (byte2 == -1)
            {
                throw new EndOfStreamException();
            }

            return (ushort)(byte1 | (byte2 << 8));
        }

        internal static byte[] ReadBytes(this Stream stream, int length)
        {
            byte[] bytes = new byte[length];

            int bytesRead = 0;
            int bytesToRead = length;

            do
            {
                int read = stream.Read(bytes, bytesRead, bytesToRead);

                bytesRead += read;
                bytesToRead -= read;

            } while (bytesToRead > 0);

            return bytes;
        }
        
    }
}
