﻿using System;
using System.Security.Cryptography;
using System.Text;
using Intersect;
using Intersect_Client.Classes.Entities;
using Intersect_Client.Classes.General;
using Intersect_Client.Classes.Maps;
using Intersect_Client.Classes.UI.Game;

namespace Intersect_Client.Classes.Networking
{
    public static class PacketSender
    {
        public static void SendPing()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.Ping);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendLogin(string username, string password)
        {
            var bf = new ByteBuffer();
            var sha = new SHA256Managed();
            bf.WriteLong((int) ClientPackets.Login);
            bf.WriteString(username.ToLower().Trim());
            bf.WriteString(password.Trim());
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendNeedMap(int mapNum)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.NeedMap);
            bf.WriteLong(mapNum);
            GameNetwork.SendPacket(bf.ToArray());
            if (MapInstance.MapRequests.ContainsKey(mapNum))
            {
                MapInstance.MapRequests[mapNum] = Globals.System.GetTimeMS() + 3000;
            }
            else
            {
                MapInstance.MapRequests.Add(mapNum, Globals.System.GetTimeMS() + 3000);
            }
        }

        public static void SendMove()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.SendMove);
            bf.WriteInteger(Globals.Me.CurrentMap);
            bf.WriteInteger(Globals.Me.CurrentX);
            bf.WriteInteger(Globals.Me.CurrentY);
            bf.WriteInteger(Globals.Me.Dir);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendChatMsg(string msg)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.LocalMessage);
            bf.WriteString(msg);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendAttack(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TryAttack);
            bf.WriteLong(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendBlock(int blocking)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TryBlock);
            bf.WriteInteger(blocking);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendDir(int dir)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.SendDir);
            bf.WriteLong(dir);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendEnterGame()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.EnterGame);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendActivateEvent(int mapNum, int eventIndex)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.ActivateEvent);
            bf.WriteInteger(mapNum);
            bf.WriteInteger(eventIndex);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendEventResponse(int response, EventDialog ed)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.EventResponse);
            bf.WriteInteger(ed.EventMap);
            bf.WriteInteger(ed.EventIndex);
            bf.WriteInteger(response);
            Globals.EventDialogs.Remove(ed);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCreateAccount(string username, string password, string email)
        {
            var bf = new ByteBuffer();
            var sha = new SHA256Managed();
            bf.WriteLong((int) ClientPackets.CreateAccount);
            bf.WriteString(username.Trim());
            bf.WriteString(BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(password.Trim())))
                .Replace("-", ""));
            bf.WriteString(email.Trim());
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCreateCharacter(string Name, int Class, int Sprite)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CreateCharacter);
            bf.WriteString(Name.Trim());
            bf.WriteInteger(Class);
            bf.WriteInteger(Sprite);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPickupItem(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PickupItem);
            bf.WriteInteger(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendSwapItems(int item1, int item2)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.SwapItems);
            bf.WriteInteger(item1);
            bf.WriteInteger(item2);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendDropItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.DropItems);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendUseItem(int slot)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.UseItem);
            bf.WriteInteger(slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendSwapSpells(int spell1, int spell2)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.SwapSpells);
            bf.WriteInteger(spell1);
            bf.WriteInteger(spell2);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendForgetSpell(int slot)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.ForgetSpell);
            bf.WriteInteger(slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendUseSpell(int slot, int target)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.UseSpell);
            bf.WriteInteger(slot);
            bf.WriteInteger(target);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendUnequipItem(int slot)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.UnequipItem);
            bf.WriteInteger(slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendUpgradeStat(int stat)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.UpgradeStat);
            bf.WriteInteger(stat);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendHotbarChange(int slot)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.HotbarChange);
            bf.WriteInteger(slot);
            bf.WriteInteger(Globals.Me.Hotbar[slot].Type);
            bf.WriteInteger(Globals.Me.Hotbar[slot].Slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendOpenAdminWindow()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.OpenAdminWindow);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendAdminAction(int action, string val1 = "", string val2 = "", string val3 = "",
            string val4 = "")
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.AdminAction);
            bf.WriteInteger(action);
            bf.WriteString(val1);
            bf.WriteString(val2);
            bf.WriteString(val3);
            bf.WriteString(val4);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendSellItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.SellItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendBuyItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.BuyItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCloseShop()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CloseShop);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendDepositItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.DepositItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendWithdrawItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.WithdrawItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCloseBank()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CloseBank);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCloseCraftingBench()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CloseCraftingBench);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendMoveBankItems(int slot1, int slot2)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.MoveBankItem);
            bf.WriteInteger(slot1);
            bf.WriteInteger(slot2);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCraftItem(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CraftItem);
            bf.WriteInteger(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPartyInvite(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PartyInvite);
            bf.WriteInteger(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPartyKick(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PartyKick);
            bf.WriteInteger(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPartyLeave()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PartyLeave);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPartyAccept(Object sender, EventArgs e)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PartyAcceptInvite);
            bf.WriteInteger((int) ((InputBox) sender).Slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendPartyDecline(Object sender, EventArgs e)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.PartyDeclineInvite);
            bf.WriteInteger((int) ((InputBox) sender).Slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendAcceptQuest(int questId)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.AcceptQuest);
            bf.WriteInteger(questId);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendDeclineQuest(int questId)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.DeclineQuest);
            bf.WriteInteger(questId);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCancelQuest(int questId)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CancelQuest);
            bf.WriteInteger(questId);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendTradeRequest(int index)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeRequest);
            bf.WriteInteger(index);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendOfferItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeOffer);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendRevokeItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeRevoke);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendAcceptTrade()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeAccept);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendDeclineTrade()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeDecline);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendTradeRequestAccept(Object sender, EventArgs e)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeRequestAccept);
            bf.WriteInteger((int) ((InputBox) sender).Slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendTradeRequestDecline(Object sender, EventArgs e)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.TradeRequestDecline);
            bf.WriteInteger((int) ((InputBox) sender).Slot);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendStoreBagItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.StoreBagItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendRetreiveBagItem(int slot, int amount)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.RetreiveBagItem);
            bf.WriteInteger(slot);
            bf.WriteInteger(amount);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendCloseBag()
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.CloseBag);
            GameNetwork.SendPacket(bf.ToArray());
        }

        public static void SendMoveBagItems(int slot1, int slot2)
        {
            var bf = new ByteBuffer();
            bf.WriteLong((int) ClientPackets.MoveBagItem);
            bf.WriteInteger(slot1);
            bf.WriteInteger(slot2);
            GameNetwork.SendPacket(bf.ToArray());
        }
    }
}