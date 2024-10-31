using System;
using System.Collections.Generic;

namespace DabaBase.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? Text { get; set; }

    public int? ProductId { get; set; }

    public string? Reply { get; set; }

    public int? CustomerId { get; set; }

    public int? ReplyingId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
