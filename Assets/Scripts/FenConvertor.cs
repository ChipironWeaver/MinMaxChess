using UnityEngine;

public class FenConvertor : MonoBehaviour
{
    public static int[] GetGridFromFen(string fen)
    {
        int[] grid = new int[64];
        for (int i = 0; i < 64; i++)
        {
            grid[i] = -1;
        }

        int index = 0;
        string lowerFen = fen.ToLower();
        
        print(lowerFen);
        print(fen);
        
        for(int i = 0; i < fen.Length; i++)
        {
            bool isPiece = false;
            char c = lowerFen[i];
            switch (c)
            {
                case 'p':
                    grid[index] = 0;
                    isPiece = true;
                    break;
                case 'b':
                    grid[index] = 2;
                    isPiece = true;
                    break;
                case 'n':
                    grid[index] = 4;
                    isPiece = true;
                    break;
                case 'r':
                    grid[index] = 6;
                    isPiece = true;
                    break;
                case 'q':
                    grid[index] = 8;
                    isPiece = true;
                    break;
                case 'k':
                    grid[index] = 10;
                    isPiece = true;
                    break;
                case '/':
                    index += index % 8 == 0 ? 0 : index - index % 8 + 8;
                    break;
                case ' ':
                    return grid;
                default:
                    if (!int.TryParse(c.ToString(), out int number))
                    {
                        print(c);
                        break;
                    }
                    index += number;
                    print((int)c + " " + c);
                    break;
            }

            if (isPiece)
            {
                if (char.IsLower(fen[i]))
                {
                    grid[index]++;
                }

                index++;
            }
        }
        
        return grid;
    }
}
