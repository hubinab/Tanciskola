using Microsoft.EntityFrameworkCore;
using Repo.models;

VizsgaContext tarolo = new VizsgaContext();

Console.WriteLine($"5. feladat: {tarolo.Orarends.Count(x => x.Tanar2 != null)} órát tart 2 tánctanár");

Console.Write("6. feladat: A tánctanár neve: ");

string nev = Console.ReadLine() ?? "";

var tanctanar = tarolo.Tanars.FirstOrDefault(x => x.Nev == nev);

if  (tanctanar == null)
{
    Console.WriteLine("\tIlyen néven nem található tánctanár");
}
else
{
    Console.WriteLine($"\tEmail: {tanctanar.Email}");
}

Console.WriteLine("7. feladat: A 3 legtöbbet kereső tánctanár:");

var top3 = tarolo.Tanars
    .Include(x => x.OrarendTanar1Navigations).ThenInclude(x => x.SzintNavigation)
    .Include(x => x.OrarendTanar2Navigations).ThenInclude(x => x.SzintNavigation)
    .ToList().OrderByDescending(x => x.BevOssz).Take(3);

foreach (var item in top3)
{
    Console.WriteLine($"\t{item.Nev}: {item.BevOssz} Ft");
}