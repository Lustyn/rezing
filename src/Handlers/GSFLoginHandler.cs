using System;
using System.Collections.Generic;
using Service = GSFLoginSvc;

public class GSFLoginHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{
    private static GSFOID oid = new GSFOID(0L);

    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            siteInfo = new GSFSiteInfo
            {
                siteId = oid,
                nicknameFirst = "nicknameFirst",
                nicknameLast = "nicknameLast",
                siteUserId = oid,
            },
            status = GSFSessionStatus.IN_PROGRESS,
            sessionID = oid,
            conversationID = 0,
            assetDeliveryURL = "http://localhost:3000",
            player = new GSFPlayer
            {
                oid = oid,
                activePlayerAvatar = new GSFPlayerAvatar
                {
                    avatar = new GSFAvatar
                    {
                        oid = oid,
                        dimensions = "dimensions",
                        weight = 0.0,
                        height = 0.0,
                        maxOutfits = 0,
                        name = "name",
                        assetMap = new Dictionary<string, List<GSFAsset>>(),
                        assetPackages = new List<GSFAssetPackage>(),
                    },
                    playerID = oid,
                    name = "name",
                    bio = "bio",
                    secretCode = "secret",
                    playerAvatarOutfitId = oid,
                    outfitNo = 0,
                },
                homeThemeId = oid,
                currentRaceMode = new GSFRaceMode
                {
                    oid = oid,
                    name = "raceMode",
                },
                workshopOptions = "workshopOptions",
                isTutorialCompleted = true,
                yardBuildingID = oid,
                isQA = false,
                homeVillagePlotID = oid,
                storeVillagePlotID = oid,
                playerStoreID = oid,
                playerMazeID = oid,
                villageID = oid,
                playerName = "playerName",
            },
            maxOutfit = 0,
            playerStats = new List<GSFPlayerStats>(),
            playerInfoTO = new GSFPlayerInfoTO
            {
                tierID = oid,
                playerName = "playerName",
                worldName = "worldName",
                crispDataTO = new GSFCrispDataTO
                {
                    crispActionID = oid,
                    crispMessage = "crispMessage",
                    crispConfirmed = false,
                },
                verified = true,
                verificationExpiry = DateTime.Now,
                eulaID = oid,
                currentEulaID = oid,
                u13 = false,
                chatBlockedParent = false,
                chatAllowed = true,
                findable = false,
                qa = false,
                playerSettings = new List<GSFPlayerSetting>(),
            },
            currentServerTime = DateTime.Now,
            clientInactivityTimeout = 30,
            cnl = "steam",
        };
    }
}
