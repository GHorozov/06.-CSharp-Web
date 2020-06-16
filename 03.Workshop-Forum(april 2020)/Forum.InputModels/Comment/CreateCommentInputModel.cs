using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.InputModels.Comment
{
    public class CreateCommentInputModel
    {
        public string Content { get; set; }

        public string PostId { get; set; }

        public string ParentId { get; set; }
    }
}
