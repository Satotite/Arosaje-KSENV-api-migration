using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Plante
{
    public int IdPlante { get; set; }

    public string? Espece { get; set; }

    public string? Description { get; set; }

    public string? Categorie { get; set; }

    public string? Etat { get; set; }

    public string? Nom { get; set; }

    public string? Lon { get; set; }

    public string? Lat { get; set; }

    public int IdVille { get; set; }

    public int? IdPhoto { get; set; }

    public int IdUtilisateur { get; set; }

    public int? IdUtilisateur1 { get; set; }

    public virtual Photo? IdPhotoNavigation { get; set; }

    public virtual Membre IdUtilisateur1Navigation { get; set; } = null!;

    public virtual Membre IdUtilisateurNavigation { get; set; } = null!;

    public virtual Ville IdVilleNavigation { get; set; } = null!;

    public virtual ICollection<DateTip> IdTips { get; set; } = new List<DateTip>();
}
