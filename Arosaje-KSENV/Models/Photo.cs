using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Photo
{
    public int IdPhoto { get; set; }

    public string? Image { get; set; }
    public string? extension { get; set; }

    public virtual ICollection<Plante> Plantes { get; set; } = new List<Plante>();

    public virtual ICollection<Membre> IdUtilisateurs { get; set; } = new List<Membre>();
}
