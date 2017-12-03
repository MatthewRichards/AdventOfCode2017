﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            Day3Part2();

            Console.In.ReadLine();
        }

        enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }

        private static void Day3Part1()
        {
            int targetSquare = 361527;

            int currentSquare = 1;
            int howRight = 0;
            int howUp = 0;
            int spiralSize = 1;
            int howSpiralled = 0;
            Direction direction = Direction.Right;
             
            while (currentSquare < targetSquare)
            {
                currentSquare++;

                switch (direction)
                {
                    case Direction.Right:
                        howRight++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Up;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Up:
                        howUp++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Left;
                            howSpiralled = 0;
                            spiralSize++;
                        }
                        break;

                    case Direction.Left:
                        howRight--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Down;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Down:
                        howUp--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Right;
                            howSpiralled = 0;
                            spiralSize++;
                        }

                        break;
                        }

                }

            int answer = Math.Abs(howRight) + Math.Abs(howUp);
            Console.WriteLine($"Answer is {answer} because we've gone right {howRight} and up {howUp}");

            }

        private static void Day3Part2()
        {
            int targetSquare = 361527;

            int currentSquare = 1;
            int howRight = 0;
            int howUp = 0;
            int spiralSize = 1;
            int howSpiralled = 0;
            Direction direction = Direction.Right;

            // We store (howRight, howUp) => value
            var storedValues = new Dictionary<Tuple<int, int>, int>();

            storedValues[Tuple.Create(0, 0)] = 1; // By definition

            while (currentSquare < targetSquare)
            {
                currentSquare++;

                switch (direction)
                {
                    case Direction.Right:
                        howRight++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Up;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Up:
                        howUp++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Left;
                            howSpiralled = 0;
                            spiralSize++;
                        }
                        break;

                    case Direction.Left:
                        howRight--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Down;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Down:
                        howUp--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Right;
                            howSpiralled = 0;
                            spiralSize++;
                        }

                        break;
                }

                // Find all the adjacent squares that have values, add them up, 
                // and store them in this square, which shouldn't already have a value


                var thisSquare = Tuple.Create(howRight, howUp);

                if (storedValues.ContainsKey(thisSquare)) throw new InvalidOperationException("Oops, tried to store a value twice");
                
                var adjacentSquares = new[]
                {Tuple.Create(howRight +1,howUp),
                Tuple.Create(howRight -1,howUp),
                Tuple.Create(howRight, howUp+1),
                Tuple.Create(howRight, howUp-1),
                Tuple.Create(howRight+1, howUp+1),
                Tuple.Create(howRight +1,howUp-1),
                Tuple.Create(howRight-1,howUp+1),
                Tuple.Create(howRight-1,howUp-1)};

                var valueToStore = adjacentSquares
                    .Where(square => storedValues.ContainsKey(square))
                    .Sum(square => storedValues[square]);
                Console.WriteLine($"Square:{currentSquare} at Right: {howRight}, Up: {howUp}, storing value: {valueToStore}");
                storedValues[thisSquare] = valueToStore;

                if (valueToStore > targetSquare)
                {
                    Console.WriteLine($"Answer: {valueToStore}");
                    return;
                }
            }

        }

        static void Day1Part1()

        {
            int sum = 0;

            const string Input = "77736991856689225253142335214746294932318813454849177823468674346512426482777696993348135287531487622845155339235443718798255411492778415157351753377959586612882455464736285648473397681163729345143319577258292849619491486748832944425643737899293811819448271546283914592546989275992844383947572926628695617661344293284789225493932487897149244685921644561896799491668147588536732985476538413354195246785378443492137893161362862587297219368699689318441563683292683855151652394244688119527728613756153348584975372656877565662527436152551476175644428333449297581939357656843784849965764796365272113837436618857363585783813291999774718355479485961244782148994281845717611589612672436243788252212252489833952785291284935439662751339273847424621193587955284885915987692812313251556836958571335334281322495251889724281863765636441971178795365413267178792118544937392522893132283573129821178591214594778712292228515169348771198167462495988252456944269678515277886142827218825358561772588377998394984947946121983115158951297156321289231481348126998584455974277123213413359859659339792627742476688827577318285573236187838749444212666293172899385531383551142896847178342163129883523694183388123567744916752899386265368245342587281521723872555392212596227684414269667696229995976182762587281829533181925696289733325513618571116199419759821597197636415243789757789129824537812428338192536462468554399548893532588928486825398895911533744671691387494516395641555683144968644717265849634943691721391779987198764147667349266877149238695714118982841721323853294642175381514347345237721288281254828745122878268792661867994785585131534136646954347165597315643658739688567246339618795777125767432162928257331951255792438831957359141651634491912746875748363394329848227391812251812842263277229514125426682179711184717737714178235995431465217547759282779499842892993556918977773236196185348965713241211365895519697294982523166196268941976859987925578945185217127344619169353395993198368185217391883839449331638641744279836858188235296951745922667612379649453277174224722894599153367373494255388826855322712652812127873536473277";

            var InputNumbers = Input.Select(c => int.Parse(c + "")).ToArray();

            for (int currentIndex = 0; currentIndex < InputNumbers.Length; currentIndex++)
            {
                var nextIndex = currentIndex + 1;
                if (nextIndex >= InputNumbers.Length) nextIndex = 0;

                if (InputNumbers[currentIndex] == InputNumbers[nextIndex])
                {
                    sum += InputNumbers[currentIndex];
                }
            }

            Console.WriteLine(sum);


        }

        static void Day1Part2()

        {
            int sum = 0;

            const string Input = "77736991856689225253142335214746294932318813454849177823468674346512426482777696993348135287531487622845155339235443718798255411492778415157351753377959586612882455464736285648473397681163729345143319577258292849619491486748832944425643737899293811819448271546283914592546989275992844383947572926628695617661344293284789225493932487897149244685921644561896799491668147588536732985476538413354195246785378443492137893161362862587297219368699689318441563683292683855151652394244688119527728613756153348584975372656877565662527436152551476175644428333449297581939357656843784849965764796365272113837436618857363585783813291999774718355479485961244782148994281845717611589612672436243788252212252489833952785291284935439662751339273847424621193587955284885915987692812313251556836958571335334281322495251889724281863765636441971178795365413267178792118544937392522893132283573129821178591214594778712292228515169348771198167462495988252456944269678515277886142827218825358561772588377998394984947946121983115158951297156321289231481348126998584455974277123213413359859659339792627742476688827577318285573236187838749444212666293172899385531383551142896847178342163129883523694183388123567744916752899386265368245342587281521723872555392212596227684414269667696229995976182762587281829533181925696289733325513618571116199419759821597197636415243789757789129824537812428338192536462468554399548893532588928486825398895911533744671691387494516395641555683144968644717265849634943691721391779987198764147667349266877149238695714118982841721323853294642175381514347345237721288281254828745122878268792661867994785585131534136646954347165597315643658739688567246339618795777125767432162928257331951255792438831957359141651634491912746875748363394329848227391812251812842263277229514125426682179711184717737714178235995431465217547759282779499842892993556918977773236196185348965713241211365895519697294982523166196268941976859987925578945185217127344619169353395993198368185217391883839449331638641744279836858188235296951745922667612379649453277174224722894599153367373494255388826855322712652812127873536473277";

            var InputNumbers = Input.Select(c => int.Parse(c + "")).ToArray();

            for (int currentIndex = 0; currentIndex < InputNumbers.Length; currentIndex++)
            {
                var nextIndex = currentIndex + (InputNumbers.Length / 2);
                if (nextIndex >= InputNumbers.Length) nextIndex -= InputNumbers.Length;

                if (InputNumbers[currentIndex] == InputNumbers[nextIndex])
                {
                    sum += InputNumbers[currentIndex];
                }
            }

            Console.WriteLine(sum);


        }

        static void Day2Part1()
        {
            const string Input = @"790	99	345	1080	32	143	1085	984	553	98	123	97	197	886	125	947
302	463	59	58	55	87	508	54	472	63	469	419	424	331	337	72
899	962	77	1127	62	530	78	880	129	1014	93	148	239	288	357	424
2417	2755	254	3886	5336	3655	5798	3273	5016	178	270	6511	223	5391	1342	2377
68	3002	3307	166	275	1989	1611	364	157	144	3771	1267	3188	3149	156	3454
1088	1261	21	1063	1173	278	1164	207	237	1230	1185	431	232	660	195	1246
49	1100	136	1491	647	1486	112	1278	53	1564	1147	1068	809	1638	138	117
158	3216	1972	2646	3181	785	2937	365	611	1977	1199	2972	201	2432	186	160
244	86	61	38	58	71	243	52	245	264	209	265	308	80	126	129
1317	792	74	111	1721	252	1082	1881	1349	94	891	1458	331	1691	89	1724
3798	202	3140	3468	1486	2073	3872	3190	3481	3760	2876	182	2772	226	3753	188
2272	6876	6759	218	272	4095	4712	6244	4889	2037	234	223	6858	3499	2358	439
792	230	886	824	762	895	99	799	94	110	747	635	91	406	89	157
2074	237	1668	1961	170	2292	2079	1371	1909	221	2039	1022	193	2195	1395	2123
8447	203	1806	6777	278	2850	1232	6369	398	235	212	992	7520	7304	7852	520
3928	107	3406	123	2111	2749	223	125	134	146	3875	1357	508	1534	4002	4417";

            var lines = Input.Split('\n');
            var linesOfNums = lines.Select(line => line.Split('\t').Select(num => int.Parse(num + "")));

            var answer = linesOfNums.Sum(line => line.Max() - line.Min());

            Console.WriteLine(answer);
        }

        static void Day2Part2()
        {
            const string Input = @"790	99	345	1080	32	143	1085	984	553	98	123	97	197	886	125	947
302	463	59	58	55	87	508	54	472	63	469	419	424	331	337	72
899	962	77	1127	62	530	78	880	129	1014	93	148	239	288	357	424
2417	2755	254	3886	5336	3655	5798	3273	5016	178	270	6511	223	5391	1342	2377
68	3002	3307	166	275	1989	1611	364	157	144	3771	1267	3188	3149	156	3454
1088	1261	21	1063	1173	278	1164	207	237	1230	1185	431	232	660	195	1246
49	1100	136	1491	647	1486	112	1278	53	1564	1147	1068	809	1638	138	117
158	3216	1972	2646	3181	785	2937	365	611	1977	1199	2972	201	2432	186	160
244	86	61	38	58	71	243	52	245	264	209	265	308	80	126	129
1317	792	74	111	1721	252	1082	1881	1349	94	891	1458	331	1691	89	1724
3798	202	3140	3468	1486	2073	3872	3190	3481	3760	2876	182	2772	226	3753	188
2272	6876	6759	218	272	4095	4712	6244	4889	2037	234	223	6858	3499	2358	439
792	230	886	824	762	895	99	799	94	110	747	635	91	406	89	157
2074	237	1668	1961	170	2292	2079	1371	1909	221	2039	1022	193	2195	1395	2123
8447	203	1806	6777	278	2850	1232	6369	398	235	212	992	7520	7304	7852	520
3928	107	3406	123	2111	2749	223	125	134	146	3875	1357	508	1534	4002	4417";

            var lines = Input.Split('\n');
            var linesOfNums = lines.Select(line => line.Split('\t').Select(num => int.Parse(num + "")).ToArray()).ToArray();
            var linesOfPairs = linesOfNums.Select(line => line.SelectMany(num1 => line.Select(num2 => Tuple.Create(num1, num2)).ToArray()).ToArray()).ToArray();

            var answer = linesOfPairs.Sum(line => Divide(line.Single(pair => pair.Item1 != pair.Item2 && pair.Item1 % pair.Item2 == 0)));

            Console.WriteLine(answer);
        }
        static int Divide(Tuple<int, int> pair)
        {
            return pair.Item2 > pair.Item1 ? pair.Item2 / pair.Item1 : pair.Item1 / pair.Item2;
        }

    }
}
