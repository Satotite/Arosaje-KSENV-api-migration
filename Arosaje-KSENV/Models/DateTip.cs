using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class DateTip
{
    public int IdTips { get; set; }

    public string? Contenu { get; set; }

    public virtual ICollection<Plante> IdPlantes { get; set; } = new List<Plante>();

    public virtual ICollection<Botaniste> IdUtilisateurs { get; set; } = new List<Botaniste>();
}
