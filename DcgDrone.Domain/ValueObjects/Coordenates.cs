using System;

namespace DcgDrone.Domain.ValueObjects
{
    public class Coordenates
    {
        public Coordenates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordenates(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                X = 999;
                Y = 999;
                return;
            }

            var isInteger = int.TryParse(input.Substring(0, 1), out int r);
            if (isInteger)
            {
                X = 999;
                Y = 999;
                return;
            }

            if (HasCharacter(input, "ABCDEFGHIJKMPRTUVWYZ"))
            {
                X = 999;
                Y = 999;
                return;
            }

            if (HasOverflow(input))
            {
                X = 999;
                Y = 999;
                return;
            }

            // Verifica se há passos no x
            if (HasSteps(input, "X"))
            {
                X = 999;
                Y = 999;
                return;
            }

            //Remove os passos marcados com X
            input = RemoveSteps(input, "X");

            // Monta os passos
            var nCount = Steps(input, "N");
            if (nCount < -2147483647)
            {
                X = 999;
                Y = 999;
                return;
            }

            var lCount = Steps(input, "L");
            var oCount = Steps(input, "O");
            var sCount = Steps(input, "S");

            X = lCount - oCount;
            Y = nCount - sCount;
        }

        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Verifica se possui um caracter específico no texto
        /// </summary>
        /// <param name="text"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        private bool HasCharacter(string input, string characters)
        {
            for (var i = 0; i < characters.Length; i++)
            {
                var c = input.IndexOf(characters.Substring(i, 1));
                if (c > 0)
                    return true;
            }

            return false;
        }

        private bool HasOverflow(string input)
        {
            string characters = "ABCDEFGHIJKLMNOPRSTUVWYZ";
            for (var i = 0; i < characters.Length; i++)
            {
                input = input.Replace(characters.Substring(i, 1), ",");
            }

            var steps = input.Split(',');
            int n;
            foreach (var i in steps)
            {
                if (string.IsNullOrEmpty(i))
                    continue;
                if (i.Substring(i.Length - 1, 1) == "X")
                    continue;
                var isInt = int.TryParse(i, out n);
                if (!isInt)
                    return true;
            }

            return false;
        }

        private bool HasSteps(string input, string character = "X")
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 1) == character)
                {
                    if (i < (input.Length -1))
                    {
                        int nx;
                        var nxStepCheck = int.TryParse(input.Substring(i + 1, 1), out nx);
                        if (nxStepCheck)
                            return true;
                    }
                }
            }

            return false;
        }

        private string RemoveSteps(string input, string character = "X")
        {
            var n = 0;
            var newInput = input;
            var indexOfX = newInput.IndexOf(character);

            if (indexOfX > 0)
            {
                var c = character;
                var p = indexOfX;
                var countX = 0;

                for (var i = indexOfX; i < newInput.Length; i++)
                {
                    c = newInput.Substring(p, 1);
                    if (c == character)
                    {
                        p++;
                        countX++;
                        if (p == newInput.Length)
                        {
                            newInput = newInput.Remove(indexOfX, countX);

                            int nStep;
                            var step = int.TryParse(newInput.Substring(indexOfX - countX, 1), out nStep);

                            newInput = newInput.Remove(indexOfX - countX - (step ? 1 : 0), countX + (step ? 1 : 0));
                        }
                    }
                    else
                    {
                        newInput = newInput.Remove(indexOfX, countX);
                        for (var x = 0; x < countX; x++)
                        {
                            for (var s = indexOfX - countX; s >= 0; s--)
                            {
                                if (int.TryParse(newInput.Substring(s, 1), out n))
                                {
                                    newInput = newInput.Remove(s, 1);
                                }
                                else
                                {
                                    newInput = newInput.Remove(s, 1);
                                    break;
                                }
                            }
                        }

                        indexOfX = newInput.IndexOf(character);
                        if (indexOfX > 0)
                        {
                            c = character;
                            p = indexOfX;
                            countX = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return newInput;
        }

        /// <summary>
        /// Conta os passos por caracter
        /// </summary>
        /// <param name="text"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        private static int Steps(string input, string character)
        {
            int n;
            var indexOf = input.IndexOf(character);
            if (indexOf < 0)
                return 0;
            var count = 0;

            while (indexOf < input.Length)
            {
                if (input.Substring(indexOf, 1) == character && (indexOf + 1) == input.Length)
                {
                    count += 1;
                    indexOf += 1;
                }
                else if (input.Substring(indexOf, 1) == character && !int.TryParse(input.Substring(indexOf + 1, 1), out n))
                {
                    count += 1;
                    indexOf += 1;
                }
                else if (input.Substring(indexOf, 1) == character && int.TryParse(input.Substring(indexOf + 1, 1), out n))
                {
                    if (indexOf < (input.Length - 1) && int.TryParse(input.Substring(indexOf + 1, 1), out n))
                    {
                        indexOf++;
                        var steps = "";
                        while (indexOf < input.Length && int.TryParse(input.Substring(indexOf, 1), out n))
                        {
                            steps += n;
                            indexOf++;
                        }
                        count += Convert.ToInt32(steps);
                    }
                }
                else
                {
                    indexOf++;
                }
            }

            return count;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}