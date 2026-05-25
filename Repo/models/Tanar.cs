using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Tanar
{
    public int TanarId { get; set; }

    public string Nev { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Orarend> OrarendTanar1Navigations { get; set; } = new List<Orarend>();

    public virtual ICollection<Orarend> OrarendTanar2Navigations { get; set; } = new List<Orarend>();

    public double BevOssz => OrarendTanar1Navigations.Sum(x => x.Bevetel / x.Tanarszam) * 0.5 + OrarendTanar2Navigations.Sum(x => x.Bevetel / x.Tanarszam) * 0.5;
}
