using System;
using LabWork.Builders;
using LabWork.Directors;
using LabWork.Models;

namespace LabWork
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Приклад з TextPacketBuilder через Director
                var textBuilder = new TextPacketBuilder();
                var director = new PacketDirector();

                Packet textPacket = director.BuildTextMessage(
                    textBuilder,
                    from: "Client1",
                    to: "ServerA",
                    message: "Hello from Builder!"
                );

                Console.WriteLine(textPacket);
                Console.WriteLine();

                // Приклад з BinaryPacketBuilder без Director
                var binaryBuilder = new BinaryPacketBuilder();
                Packet binaryPacket = binaryBuilder
                    .SetSource("Sensor42")
                    .SetDestination("CollectorX")
                    .SetBinaryPayload(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF })
                    .Build();

                Console.WriteLine(binaryPacket);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
