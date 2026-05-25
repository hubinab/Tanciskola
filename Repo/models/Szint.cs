using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Szint
{
    public int SzintId { get; set; }

    public string Kategoria { get; set; } = null!;

    public int Ar { get; set; }

    public virtual ICollection<Orarend> Orarends { get; set; } = new List<Orarend>();
}
