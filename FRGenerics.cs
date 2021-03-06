﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using TinyTween;
using FRGenerics.Lab.Phone;
using NativeUI;

namespace FRGenerics
{
    public enum MissingHash : ulong
    {
        _DESTROY_HEAD_DISPLAY = 0x31698AA80E0223F8L,
        _SET_HEAD_DISPLAY_HEALTH_BAR_VALUE = 0x3158C77A7E888AB4L,
        _SET_HEAD_DISPLAY_FLAG_ALPHA = 0xD48FE545CD46F857L,
        _SET_HEAD_DISPLAY_FLAG_COLOR = 0x613ED644950626AEL,
        _SET_HEAD_DISPLAY_AUDIO_SPEAKER_STRING = 0x7B7723747CCB55B6L,
        _LOAD_INTERIOR = 0x2CA429C029CCF247L
    }

    public class TestConfig
    {
        public string TestValue { get; set; }
    }

    //public static class Scripts {
    //  public static void TeleportToWaypoint() {
    //    Blip wpBlip = new Blip(Function.Call<int>(Hash.GET_FIRST_BLIP_INFO_ID, 8));

    //    if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE)) {
    //      Vector3 wpVec = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, wpBlip);
    //      Game.PlayerPed.Position = wpVec;
    //    } else {
    //      Screen.ShowSubtitle("Waypoint not active.");
    //    }
    //  }

    //  public static void SpawnVehicle(string vehiclename) {
    //    Model model = new Model(vehiclename);
    //    model.Request(1000);
    //    World.CreateVehicle(model, Game.PlayerPed.Position + Game.PlayerPed.ForwardVector * 5);
    //  }
    //}

    public class FRGenerics : BaseScript
    {
        //CustomiFruit ifruit;

        public FRGenerics()
        {
            EntityDecoration.RegisterProperty(PlayerProperties.InteriorId, DecorationType.Int);
            EntityDecoration.RegisterProperty(PlayerProperties.InteriorOwner, DecorationType.Int);
            EntityDecoration.RegisterProperty(PlayerProperties.Flags, DecorationType.Int);

            EventHandlers["onClientMapStart"] += new Action<dynamic>((res) =>
            {
                Function.Call(Hash._LOAD_MP_DLC_MAPS);
                Function.Call(Hash._ENABLE_MP_DLC_MAPS, true);

                Function.Call(Hash.REQUEST_IPL, "DT1_03_Gr_Closed");
                Function.Call(Hash.REQUEST_IPL, "TrevorsTrailer");
                Function.Call(Hash.REQUEST_IPL, "v_tunnel_hole");
                Function.Call(Hash.REQUEST_IPL, "FIBlobbyfake");
                Function.Call(Hash.REQUEST_IPL, "TrevorsMP");
                Function.Call(Hash.REQUEST_IPL, "farm");
                Function.Call(Hash.REQUEST_IPL, "farmint");
                Function.Call(Hash.REQUEST_IPL, "farm_props");
                Function.Call(Hash.REQUEST_IPL, "farmint_cap");
                Function.Call(Hash.REQUEST_IPL, "CS1_02_cf_offmission");
            });

            //Tick += OnTick;

            //ifruit = new CustomiFruit() {
            //  CenterButtonColor = KnownColorTable.GimmeTheColor(KnownColor.Orange),
            //  LeftButtonColor = KnownColorTable.GimmeTheColor(KnownColor.LimeGreen),
            //  RightButtonColor = KnownColorTable.GimmeTheColor(KnownColor.Purple),
            //  CenterButtonIcon = SoftKeyIcon.Fire,
            //  LeftButtonIcon = SoftKeyIcon.Police,
            //  RightButtonIcon = SoftKeyIcon.Website
            //};

            //ifruit.SetWallpaper(Wallpaper.BadgerDefault);

            //var contact = new iFruitContact("Spawn Adder", 19);
            //contact.Answered += Contact_Answered;
            //contact.DialTimeout = 8000;
            //contact.Active = true;

            ////set custom icons by instantiating the ContactIcon class
            //contact.Icon = new ContactIcon("char_sasquatch");

            //ifruit.Contacts.Add(contact);

            //contact = new iFruitContact("Teleport to Waypoint", 20);
            //contact.Answered += (s) => Scripts.TeleportToWaypoint();
            //contact.DialTimeout = 0;
            //contact.Icon = ContactIcon.Target;

            //ifruit.Contacts.Add(contact);

            //Function.Call(Hash.CREATE_MOBILE_PHONE);

            //var pos = Game.PlayerPed.GetOffsetPosition(new Vector3(1f, 0, 0));

            //Function.Call(Hash.SET_MOBILE_PHONE_POSITION, pos.X, pos.Y, pos.Z);


            //Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, "custom_images");

            //frng = new Fringe();
            //frng.Players = Players;
            Tick += OnTick21;

            //IsCreated = false;
            //Tick += OnTickRT;
        }

        protected bool IsCreated { get; set; }
        protected Prop Prp { get; set; }

        protected Text txt = new Text("YAY", new PointF(10, 10), 1f);
        protected Rectangle rect = new Rectangle(new PointF(0, 0), new SizeF(200f, 200f), Color.FromArgb(255, 0, 0));

        public async Task OnTickRT()
        {
            //Debug.WriteLine("TickRT");

            //if (!IsCreated)
            //{
            //    Prp = await World.CreateProp(new Model("prop_npc_phone"), Game.PlayerPed.GetOffsetPosition(new Vector3(0, 2f, 0)), false, false);
            //    Prp.IsPositionFrozen = true;
            //    Prp.MarkAsNoLongerNeeded();
            //    IsCreated = true;

                
            //}

            if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "__rt"))
            {
                Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "__rt", 0);
                Function.Call(
                    Hash.LINK_NAMED_RENDERTARGET,
                    //557686077 // screen
                608950395 // ex_tvscreen
                //0xc2161726 // prop_npc_phone
                );
            }
            else if (!IsCreated)
            {
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "__rt");
                IsCreated = true;
                Debug.WriteLine($"Recreate RT");
            }

            int renderTarget = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "__rt");

            //Debug.WriteLine($"Render target ID: {renderTarget}");

            Function.Call(Hash.SET_TEXT_RENDER_ID, new IntPtr(renderTarget));
            txt.Draw();
            rect.Draw();
            Function.Call((Hash)0xE3A3DB414A373DAB);
            Function.Call(Hash.SET_TEXT_RENDER_ID, 1);

            //if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, "fukken_phone"))
            //{
            //    Function.Call(Hash.LINK_NAMED_RENDERTARGET, 0xc2161726);
            //    Debug.WriteLine("RT not linked");
            //}
        }

        protected volatile Fringe frng;

        public async Task OnTick3()
        {

            //ifruit.Update();

            //var cf = new Scaleform("cellphone_ifruit");

            //cf.Render2DScreenSpace(new PointF(1000, 400), new PointF(200, 300));

            //var hour = World.CurrentDayTime.Hours;
            //var minute = World.CurrentDayTime.Minutes;
            //var day = World.CurrentDayTime.Days;

            //cf.CallFunction("SET_TITLEBAR_TIME", hour, minute, day);

            await Task.FromResult(0);
        }

        //private void Contact_Answered(iFruitContact contact) {
        //  Scripts.SpawnVehicle("ADDER");
        //  Screen.ShowNotification("Your Adder has been delivered!");
        //}

        protected Scaleform missionMarker;
        protected Scaleform sff;

        protected volatile bool set = false;

        protected bool phoneOpen = false;
        protected bool phoneOpening = false;
        protected bool phoneClosing = false;
        protected Tween<float> phoneYrot = new FloatTween();
        protected Tween<float> phoneYpos = new FloatTween();

        protected float phoneYAngle = 0f;

        internal async Task LoadTextureDict(string txd)
        {
            if (!Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, txd))
            {
                Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, txd, 0);
                await Delay(0);
            }
        }

        internal void SetWallpaperTXD(string textureDict)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, sff.Handle, "SET_BACKGROUND_CREW_IMAGE");
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_2000");
            Function.Call(Hash._ADD_TEXT_COMPONENT_ITEM_STRING, textureDict);
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        protected Phone phone = new Phone();


        protected TimerBarPool tbp = new TimerBarPool();
        protected BarTimerBar btb = new BarTimerBar("WEEBING");
        protected BarTimerBar btb2 = new BarTimerBar("WEEBING2");
        protected TextTimerBar tmb = new TextTimerBar("Team1", "150");

        public async Task OnTick21()
        {
            if (Game.IsControlJustReleased(1, Control.PhoneUp))
            {
                phone.Open();
            }

            if (Game.IsControlJustReleased(1, Control.FrontendCancel))
            {
                phone.Close();
            }

            await phone.Update();




            // HUH
            //if (!set)
            //{
            //    set = true;

            //    sff = new Scaleform('mp_bigmessage');
            //}
            //BigMessageThread.MessageInstance.ShowRankupMessage("Yay", "subtitle", 100500);
            //BigMessageThread.MessageInstance.ShowMpMessageLarge("Masssage?");
            //if (!set)
            //{
            //    set = true;

            //    btb.BackgroundColor = UnknownColors.DarkMagenta;
            //    btb.ForegroundColor = UnknownColors.Magenta;
            //    btb.Percentage = 0f;
            //    tbp.Add(btb);

            //    btb2.BackgroundColor = UnknownColors.Olive;
            //    btb2.ForegroundColor = UnknownColors.DarkOliveGreen;
            //    btb2.Percentage = 0f;
            //    tbp.Add(btb2);
                
            //    tbp.Add(tmb);
            //}

            //btb.Percentage += 0.2f * Game.LastFrameTime;
            
            //if (btb.Percentage > 1f)
            //{
            //    btb.Percentage = 0f;
            //}

            //btb2.Percentage = btb.Percentage;

            //tbp.Draw();
            // /HUH


            //Debug.WriteLine("Tick21");

            //var logoTXD = "mpcarhud";
            //var logoTexture = "vehicle_card_icons_flag_japan";

            //if (!set)
            //{
            //    frng.InsideInterior(-1, Game.GenerateHash("v_garages"), Game.PlayerPed.Position);
            //    set = true;
            //}

            //if (Game.IsControlPressed(0, Control.CinematicSlowMo)) {
            //if (missionMarker == null) {
            //missionMarker = new Scaleform("mp_mission_name_freemode");
            //missionMarker = new Scaleform("mp_car_stats_01");
            //}
            //var missionMarker = new Scaleform("breaking_news");
            //var missionMarker = new Scaleform("cellphone_ifruit");

            //missionMarker = new Scaleform("organisation_name");

            //if (!set && missionMarker.IsLoaded)
            //{
            //    Debug.WriteLine("SF SETUP");

            //    missionMarker.CallFunction("SET_ORGANISATION_NAME", "Weaboo", 2, 0, 0);

            //    set = true;
            //}
            //else
            //{
            //    Debug.WriteLine("SF NOT LOADED");
            //}

            //var missionMarker = new Scaleform("breaking_news_plz");

            //if (missionMarker.IsLoaded == false)
            //{
            //    Debug.WriteLine("SF not loaded");
            //    return;
            //}

            //if (!set) {
            //  missionMarker.CallFunction("SET_TEXT", "Kng", "Playing Stalker");

            //  set = true;
            //}

            //if (Game.IsControlJustReleased(0, Control.CinematicSlowMo)) {
            //var ped = await World.CreatePed(new Model(PedHash.Skater01AFY), Game.PlayerPed.GetOffsetPosition(new Vector3(2f, 0f, 0f)));
            //var ped = World.CreateRandomVehicle(Game.PlayerPed.GetOffsetPosition(new Vector3(3f, 0f, 0f)));

            //ped.Opacity = 150;

            //Screen.Fading.FadeOut(100);
            //await Delay(50);

            //var gp1 = new Vector3(-800.8359985351562f, 337.37799072265625f, 205.24429321289062f);
            //Game.PlayerPed.Position = gp1;

            //var interiorId = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.PlayerPed);

            //Function.Call((Hash) 0x2CA429C029CCF247, interiorId);

            //Debug.WriteLine("Interior id: " + interiorId);

            //while (Function.Call<bool>(Hash.IS_INTERIOR_READY, interiorId) == false) {
            //  await Delay(0);
            //}

            //Screen.Fading.FadeOut(100);
            //}

            //if (!set)
            //{
            //    var vehicleInfo = "Title";
            //    var vehicleDetails = "Kawaiiness";
            //    //"custom_images",
            //    //"girl1",
            //    var logoTXD = "mpcarhud";
            //    //var logoTXD = "custom_images";
            //    var logoTexture = "vehicle_card_icons_flag_japan";
            //    //var logoTexture = "girl1";

            //    var stat1Str = "Nisekoi";
            //    var stat1Val = 21;

            //    var stat2Str = "Chuunibyou";
            //    var stat2Val = 41;

            //    var stat3Str = "Clannad";
            //    var stat3Val = 61;

            //    var stat4Str = "No game no life";
            //    var stat4Val = 81;

            //    missionMarker.CallFunction(
            //      "SET_VEHICLE_INFOR_AND_STATS",
            //      vehicleInfo, vehicleDetails, logoTXD, logoTexture,
            //      stat1Str, stat2Str, stat3Str, stat4Str,
            //      stat1Val, stat2Val, stat3Val, stat4Val
            //    );

            //    set = true;
            //}

            //if (!set && missionMarker.IsLoaded)
            //{
            //    Debug.WriteLine("SF SETUP");

            //    var name = "Unique name";
            //    var type = "<font color=\"#ff0023\">Le mission</font>";
            //    var playerInfo = "Player info123";
            //    var percentage = "";
            //    var debugValue = "";
            //    var isRockstartVerified = true;
            //    var playersRequired = "";
            //    var RP = 9999;
            //    var cash = 9999;
            //    var time = "123";

            //    missionMarker.CallFunction(
            //      "SET_MISSION_INFO",
            //      name, type, playerInfo,
            //      percentage, debugValue, isRockstartVerified,
            //      playersRequired, RP, cash, time
            //    );

            //    set = true;
            //}

            //missionMarker.Render3D(
            //  Game.PlayerPed.GetOffsetPosition(new Vector3(0, 1.5f, 0f)),
            //  -Game.PlayerPed.Rotation,
            //  new Vector3(1f)
            //);
            //missionMarker.Render3D(
            //  Game.PlayerPed.GetOffsetPosition(new Vector3(0, 1.5f, 1.5f)),
            //  -Game.PlayerPed.Rotation,
            //  new Vector3(2.5f, 1.5f, 1f)
            //);
            //missionMarker.Render2D();
            //missionMarker.Render2DScreenSpace(new PointF(500, 200), new PointF(200, 500));
            //}

            await Task.FromResult(0);
        }

        public async Task OnTick1()
        {
            Vector3 gp1Entry = new Vector3(-741.5785f, -1024.63147f, 6.174452f);
            Vector3 gp1Inner = new Vector3(223.4f, -1001f, -100f);
            //var gp1 = new Vector3(228f, -1003.7f, -99f);
            var gp1 = new Vector3(-800.8359985351562f, 337.37799072265625f, 205.24429321289062f);

            if (Game.IsControlJustReleased(0, Control.CinematicSlowMo))
            {
                Game.PlayerPed.Position = gp1;
            }

            await Task.FromResult(0);
        }
    }
}
