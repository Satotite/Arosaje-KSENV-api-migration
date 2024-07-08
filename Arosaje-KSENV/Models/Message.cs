using System;
using System.Collections.Generic;

namespace Arosaje_KSENV.Models;

public partial class Message
{
    public int IdMessage { get; set; }

    public string? Contenu { get; set; }

    public DateTime? DateMessage { get; set; }

    public virtual ICollection<EnvoyerRecevoir> EnvoyerRecevoirs { get; set; } = new List<EnvoyerRecevoir>();
}
