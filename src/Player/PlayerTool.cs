namespace Owop.Game;

/// <summary>Enumeration of player tools.</summary>
public enum PlayerTool
{
    /// <summary>Default cursor. Places one pixel at a time.</summary>
    Cursor,
    /// <summary>View panning tool.</summary>
    Move,
    /// <summary>Color selection tool.</summary>
    Pipette,
    /// <summary>
    /// <b>Moderator-only</b>. 
    /// Fills an entire chunk with one color.
    /// </summary>
    Eraser,
    /// <summary>Changes the view resolution.</summary>
    Zoom,
    /// <summary>Continuously replaces adjacent pixels of a certain color.</summary>
    Fill,
    /// <summary>
    /// <b>Moderator-only.</b>
    /// Pastes an image onto the canvas.
    /// </summary>
    Paste,
    /// <summary>Canvas screenshotting tool.</summary>
    Export,
    /// <summary>Places pixels in a rasterized line.</summary>
    Line,
    /// <summary>
    /// <b>Moderator-only.</b>
    /// Protects a chunk, meaning that only players ranked <see cref="PlayerRank.Moderator"/>
    /// or above will be able to modify it.
    /// </summary>
    Protect,
    /// <summary>
    /// <b>Moderator-only.</b>
    /// Copies a section of the canvas.
    /// </summary>
    Copy,
}

/// <summary>Extension methods for <see cref="PlayerTool"/>.</summary>
public static class PlayerToolExtensions
{
    /// <summary>Returns whether this tool is only available to <see cref="PlayerRank.Moderator"/> or higher.</summary>
    /// <param name="tool">The player tool.</param>
    public static bool IsModerator(this PlayerTool tool)
        => tool switch
        {
            PlayerTool.Eraser or PlayerTool.Paste or PlayerTool.Protect or PlayerTool.Copy => true,
            _ => false
        };
}
