using DexApi.Models;

namespace DexApi.Parsers
{
    /// <summary>
    /// Parser for DEX files.
    /// </summary>
    public class DexParser
    {
        /// <summary>
        /// Method to parse a DEX meter from a DEX string.
        /// </summary>
        /// <param name="dex"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        public DexMeter ParseDexMeter(string dex, string machine)
        {
            var lines = dex.Split('\n');
            var serial = lines.FirstOrDefault(x => x.StartsWith("ID1*"))?.Split('*')[1] ?? "UNKNOWN";
            var va101 = lines.FirstOrDefault(x => x.StartsWith("VA1*"))?.Split('*')[1];
            var valor = decimal.TryParse(va101, out var result) ? result / 100m : 0;

            return new DexMeter(
                machine,
                 DateTime.UtcNow,
                 serial,
                 valor
            );
        }

        /// <summary>
        /// Method to parse lane meters from a DEX string.
        /// </summary>
        /// <param name="dex"></param>
        /// <returns></returns>
        public List<DexLaneMeter> ParseLaneMeters(string dex)
        {
            var lanes = new List<DexLaneMeter>();
            var lines = dex.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("PA1*"))
                {
                    var productId = lines[i].Split('*')[1];
                    var pa2 = lines.ElementAtOrDefault(i + 1)?.Split('*');
                    var pa7 = lines.ElementAtOrDefault(i + 5)?.Split('*');

                    if (pa2 == null || pa7 == null) continue;

                    var numberOfVends = int.TryParse(pa2[1], out var nv) ? nv : 0;
                    var valuePaid = decimal.TryParse(pa2[2], out var vp) ? vp / 100m : 0;
                    var price = decimal.TryParse(pa7[4], out var pr) ? pr / 100m : 0;

                    lanes.Add(new DexLaneMeter(
                        productId,
                        price,
                        numberOfVends,
                        valuePaid
                    ));
                }
            }

            return lanes;
        }
    }

}
