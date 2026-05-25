using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Tanc
{
    public int TancId { get; set; }

    public string TancTipus { get; set; } = null!;

    public virtual ICollection<Orarend> Orarends { get; set; } = new List<Orarend>();
}
