using System.Collections.Generic;
using System.Linq;

namespace CableRobot.Fins
{
    public struct FinsAddress
    {
        public static readonly FinsAddress Empty = new FinsAddress(0, 0);

        public byte Area { get; }
        public ushort Offset { get; }

        public FinsAddress(byte area, ushort offset)
        {
            Area = area;
            Offset = offset;
        }

        public static bool TryParse(string address, out FinsAddress finsAddress)
        {
            finsAddress = Empty;

            var areas = new Dictionary<string, byte>{
                {"D", 0x82 }, {"DM", 0x82 }, 
                {"T", 0x89 }, {"TC", 0x89 }, 
                {"E0_", 0xA0 },
                {"E1_", 0xA1 },
                {"E2_", 0xA2 },
                {"E3_", 0xA3 },
                {"E4_", 0xA4 },
                {"E5_", 0xA5 },
                {"E6_", 0xA6 },
                {"E7_", 0xA7 },
                {"E8_", 0xA8 },
                {"E9_", 0xA9 },
                {"EA_", 0xAA },
                {"EB_", 0xAB },
                {"EC_", 0xAC }, 
                {"IO", 0xB0 }, {"CIO", 0xB0 }, 
                {"W", 0xB1 }, {"WR", 0xB1 }, 
                {"H", 0xB2 }, {"HR", 0xB2 }, 
                {"A", 0xB3 }, {"AR", 0xB3 }, 
            };

            var area = areas.Keys.FirstOrDefault(address.StartsWith);
            if (area == null)
                return false;

            if (address.Length == area.Length)
            {
                finsAddress = new FinsAddress(areas[area], 0);
                return true;
            }

            if (ushort.TryParse(address.Substring(area.Length), out var offset))
            {
                finsAddress = new FinsAddress(areas[area], offset);
                return true;
            }

            return false;
        }
    }
}
