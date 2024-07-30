using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public class PermissionTreeNode
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<PermissionTreeNode> Children { get; set; } = new List<PermissionTreeNode>();
    }
}
