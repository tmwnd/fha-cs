using cs_games;
using cs_games.dame;

Dame dame = new Dame();
dame.Init();
Console.WriteLine(dame);

for (int i = 0; i < 8; i++)
    Console.WriteLine(dame.Field[5, i]?.CanMove());