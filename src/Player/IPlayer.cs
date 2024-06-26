using System.Drawing;
using Owop.Network;
using Owop.Util;

namespace Owop.Game;

/// <summary>
/// Represents a generic player within the world, either another connected player
/// or the <see cref="IClientPlayer"/>.
/// </summary>
public interface IPlayer : IPositioned
{
    /// <summary>The player's connected world.</summary>
    IWorld World { get; }

    /// <summary>The player's selected tool.</summary>
    PlayerTool Tool { get; }

    /// <summary>The player's ID.</summary>
    int Id { get; }

    /// <summary>The player's selected color.</summary>
    Color Color { get; }

    /// <summary>Whether the player is the client player.</summary>
    bool IsClient { get; }

    /// <summary>Moves the player to a raw position within the world.</summary>
    /// <param name="pos">The player's new raw position.</param>
    Task Move(Position pos);

    /// <summary>Moves the player to a pixel position within the world.</summary>
    /// <param name="worldPos">The player's new pixel position.</param>
    Task MoveWorld(Position worldPos);

    /// <summary>Kicks the player from the world.</summary>
    Task Kick();

    /// <summary>Mutes the player.</summary>
    Task Mute();

    /// <summary>Unmutes the player from the world.</summary>
    Task Unmute();

    /// <summary>Queries <see cref="WhoisData"/> from the player.</summary>
    /// <returns>An awaitable task returning the query result.</returns>
    Task<WhoisData> QueryWhois();

    /// <summary>Sets the player's rank.</summary>
    /// <param name="rank">The player's new rank.</param>
    Task SetRank(PlayerRank rank);

    /// <summary>Teleports the client player to this player.</summary>
    Task TeleportTo();

    /// <summary>Sends the player a private message.</summary>
    /// <param name="message">The message to send.</param>
    Task Tell(string message);
}
