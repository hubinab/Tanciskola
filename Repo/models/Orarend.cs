using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Orarend
{
    public int OrarendId { get; set; }

    public string Nap { get; set; } = null!;

    public string KezdoIdopont { get; set; } = null!;

    public int Hossz { get; set; }

    public int Tanc { get; set; }

    public int Szint { get; set; }

    public int Tanar1 { get; set; }

    public int? Tanar2 { get; set; }

    public int Terem { get; set; }

    public int Letszam { get; set; }

    public virtual Szint SzintNavigation { get; set; } = null!;

    public virtual Tanar Tanar1Navigation { get; set; } = null!;

    public virtual Tanar? Tanar2Navigation { get; set; }

    public virtual Tanc TancNavigation { get; set; } = null!;

    public double Bevetel => SzintNavigation.Ar * Hossz/60 * Letszam;

    public int Tanarszam => Tanar2Navigation == null ? 1 : 2; 
}
