using System.ComponentModel;

namespace MiniInventoryManagementSystem.Domain.Utilities
{
    public enum TaskStatus
    {
        Backlog,
        Doing,
        Done
    }

    public enum MessageType
    {
        [Description("Create")]
        Create,
        [Description("Update")]
        Update,
        [Description("Cancel")]
        Cancel,
        [Description("CancelItem")]
        CancelItem
    }

}
