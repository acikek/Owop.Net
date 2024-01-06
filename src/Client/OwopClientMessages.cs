﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Owop.Network;

namespace Owop.Client;

public partial class OwopClient
{
    [GeneratedRegex(@"-> (\d) tells you: (.*)")]
    private static partial Regex TellClientPattern();

    private bool _whois = false;
    private readonly List<string> _messageBuffer;

    private void HandleTextMessage(string text, WorldData world)
    {
        if (HandleTextLine(text) is ServerMessage message)
        {
            HandleServerMessage(message, world);
        }
    }

    private ServerMessage? PrefixedTextLine(ServerMessageType type, string prefix, string text)
    {
        int index = text.IndexOf(prefix, StringComparison.Ordinal);
        return index == 0 ? new(type, [text[prefix.Length..]]) : null;
    }

    private ServerMessage? HandleTextLine(string text)
    {
        string whoisHeader = "Client information for: ";
        if (text.StartsWith(whoisHeader))
        {
            _whois = true;
            _messageBuffer.Add(text[whoisHeader.Length..]);
            return null;
        }
        if (_whois)
        {
            _messageBuffer.Add(text[3..]);
            if (text.StartsWith("-> Rank"))
            {
                ServerMessage message = new(ServerMessageType.Whois, new List<string>(_messageBuffer));
                _whois = false;
                _messageBuffer.Clear();
                return message;
            }
            return null;
        }
        var prefixed = PrefixedTextLine(ServerMessageType.Info, "[Server] ", text)
            ?? PrefixedTextLine(ServerMessageType.Info, "Server: ", text)
            ?? PrefixedTextLine(ServerMessageType.Error, "Error: ", text)
            ?? PrefixedTextLine(ServerMessageType.Nickname, "Nickname ", text)
            ?? PrefixedTextLine(ServerMessageType.TellPlayer, "-> You tell ", text)
            ?? PrefixedTextLine(ServerMessageType.Ids, "Total: ", text);
        if (prefixed is not null)
        {
            return prefixed;
        }
        if (text.StartsWith('(') || text.StartsWith('[') || int.TryParse(text[0].ToString(), out int _))
        {
            return new(ServerMessageType.Chat, [text]);
        }
        var tellClient = TellClientPattern().Matches(text);
        if (tellClient.Count > 0)
        {
            var args = tellClient[0].Groups.Cast<Group>().Skip(1).Select(g => g.Value).ToList();
            return new(ServerMessageType.TellClient, args);
        }
        return new(ServerMessageType.Info, [text]);
    }

    private void HandleServerMessage(ServerMessage message, WorldData world)
    {
        Console.WriteLine($"[SERVERMSG] {message.Type}: \"{string.Join(", ", message.Args)}\"");
        switch (message.Type)
        {
            case ServerMessageType.Chat:
                Logger.LogDebug($"Received chat message: {message.Args[0]}");
                var chatMessage = ChatEventArgs.Create(world, message.Args[0]);
                Chat?.Invoke(this, chatMessage);
                break;
        }
    }
}