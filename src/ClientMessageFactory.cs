using System;

public class ClientMessageFactory : GSFBitProtocolCodec.IMessageFactory
{
    public GSFMessage BuildMessage(MessageHeader header)
    {
        switch (header.svcClass)
        {
            case 18:
                return BuildUserServerMessage(header);
            case 19:
                return BuildSyncServerMessage(header);
            case -1:
                return BuildClientMessage(header);
            default:
                throw new Exception("Invalid svc class");
        }
    }

    private GSFMessage BuildUserServerMessage(MessageHeader header)
    {
        if (header.msgType == 15)
        {
            return NewResponse<GSFLoginSvc.GSFResponse>(header);
        }

        if (header.msgType == 26)
        {
            return NewResponse<GSFReLoginSvc.GSFResponse>(header);
        }

        if (header.msgType == 28)
        {
            return NewResponse<GSFGetLangLocaleSvc.GSFResponse>(header);
        }

        if (header.msgType == 19)
        {
            return NewResponse<GSFLogoutSvc.GSFResponse>(header);
        }

        if (header.msgType == 136)
        {
            return NewResponse<GSFGetSiteFrameSvc.GSFResponse>(header);
        }

        if (header.msgType == 3)
        {
            return NewResponse<GSFRegisterPlayerSvc.GSFResponse>(header);
        }

        if (header.msgType == 18)
        {
            return NewResponse<GSFRegisterFCSSCodeSvc.GSFResponse>(header);
        }

        if (header.msgType == 23)
        {
            return NewResponse<GSFUpdateAvatarNameSvc.GSFResponse>(header);
        }

        if (header.msgType == 1)
        {
            return NewResponse<GSFGetAvatarsSvc.GSFResponse>(header);
        }

        if (header.msgType == 135)
        {
            return NewResponse<GSFUpdatePlayerActiveAvatarSvc.GSFResponse>(header);
        }

        if (header.msgType == 17)
        {
            return NewResponse<GSFCheckUsernameSvc.GSFResponse>(header);
        }

        if (header.msgType == 543)
        {
            return NewResponse<GSFValidateNameSvc.GSFResponse>(header);
        }

        if (header.msgType == 481)
        {
            return NewResponse<GSFPreFilterNameCheckAvailabilitySvc.GSFResponse>(header);
        }

        if (header.msgType == 20)
        {
            return NewResponse<GSFGetInventoryObjectsSvc.GSFResponse>(header);
        }

        if (header.msgType == 21)
        {
            return NewResponse<GSFGetBuildObjectsSvc.GSFResponse>(header);
        }

        if (header.msgType == 22)
        {
            return NewResponse<GSFGetCollectionObjectsSvc.GSFResponse>(header);
        }

        if (header.msgType == 5)
        {
            return NewResponse<GSFStartMazeEditSvc.GSFResponse>(header);
        }

        if (header.msgType == 8)
        {
            return NewResponse<GSFEndMazeEditSvc.GSFResponse>(header);
        }

        if (header.msgType == 9)
        {
            return NewResponse<GSFDeleteMazeSvc.GSFResponse>(header);
        }

        if (header.msgType == 62)
        {
            return NewResponse<GSFGetSystemMazeRatingSvc.GSFResponse>(header);
        }

        if (header.msgType == 108)
        {
            return NewResponse<GSFStartMazePlaySvc.GSFResponse>(header);
        }

        if (header.msgType == 109)
        {
            return NewResponse<GSFEndMazePlaySvc.GSFResponse>(header);
        }

        if (header.msgType == 67)
        {
            return NewResponse<GSFGetHomeMazeSvc.GSFResponse>(header);
        }

        if (header.msgType == 38)
        {
            return NewResponse<GSFAddFriendSvc.GSFResponse>(header);
        }

        if (header.msgType == 192)
        {
            return NewResponse<GSFFindPlayerByNicknameSvc.GSFResponse>(header);
        }

        if (header.msgType == 84)
        {
            return NewResponse<GSFGetFriendByFriendPlayerIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 82)
        {
            return NewResponse<GSFGetActiveFriendListSvc.GSFResponse>(header);
        }

        if (header.msgType == 36)
        {
            return NewResponse<GSFGetBlockedPlayersSvc.GSFResponse>(header);
        }

        if (header.msgType == 33)
        {
            return NewResponse<GSFGetFriendListSvc.GSFResponse>(header);
        }

        if (header.msgType == 409)
        {
            return NewResponse<GSFSetPlayerFindableSvc.GSFResponse>(header);
        }

        if (header.msgType == 396)
        {
            return NewResponse<GSFListLimitsSvc.GSFResponse>(header);
        }

        if (header.msgType == 324)
        {
            return NewResponse<GSFSuggestFriendsSvc.GSFResponse>(header);
        }

        if (header.msgType == 455)
        {
            return NewResponse<GSFListFindablePlayersSvc.GSFResponse>(header);
        }

        if (header.msgType == 83)
        {
            return NewResponse<GSFGetFriendRequestsSvc.GSFResponse>(header);
        }

        if (header.msgType == 459)
        {
            return NewResponse<GSFGetFriendshipRequestCountsSvc.GSFResponse>(header);
        }

        if (header.msgType == 81)
        {
            return NewResponse<GSFUpdateFriendCommentSvc.GSFResponse>(header);
        }

        if (header.msgType == 37)
        {
            return NewResponse<GSFManageBlockPlayerSvc.GSFResponse>(header);
        }

        if (header.msgType == 34)
        {
            return NewResponse<GSFRemoveFriendSvc.GSFResponse>(header);
        }

        if (header.msgType == 35)
        {
            return NewResponse<GSFManageFriendRequestSvc.GSFResponse>(header);
        }

        if (header.msgType == 7)
        {
            return NewResponse<GSFGetCommunityMazeSvc.GSFResponse>(header);
        }

        if (header.msgType == 49)
        {
            return NewResponse<GSFGetCommunityMazeThumbnailsSvc.GSFResponse>(header);
        }

        if (header.msgType == 12)
        {
            return NewResponse<GSFGetCommunityMazesSvc.GSFResponse>(header);
        }

        if (header.msgType == 92)
        {
            return NewResponse<GSFGetPlayerMazePlaySvc.GSFResponse>(header);
        }

        if (header.msgType == 101)
        {
            return NewResponse<GSFGetSystemMazePlaySvc.GSFResponse>(header);
        }

        if (header.msgType == 80)
        {
            return NewResponse<GSFApproveMazePublishingSvc.GSFResponse>(header);
        }

        if (header.msgType == 248)
        {
            return NewResponse<GSFGetOnlineStatusesSvc.GSFResponse>(header);
        }

        if (header.msgType == 6)
        {
            return NewResponse<GSFGetPlayerMazeSvc.GSFResponse>(header);
        }

        if (header.msgType == 4)
        {
            return NewResponse<GSFGetPlayerMazesSvc.GSFResponse>(header);
        }

        if (header.msgType == 13)
        {
            return NewResponse<GSFGetQuestMazesSvc.GSFResponse>(header);
        }

        if (header.msgType == 11)
        {
            return NewResponse<GSFGetZoneMazesSvc.GSFResponse>(header);
        }

        if (header.msgType == 63)
        {
            return NewResponse<GSFRatePlayerMazeSvc.GSFResponse>(header);
        }

        if (header.msgType == 50)
        {
            return NewResponse<GSFAcceptQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 491)
        {
            return NewResponse<GSFCreateQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 168)
        {
            return NewResponse<GSFGetPlayerQuestsSvc.GSFResponse>(header);
        }

        if (header.msgType == 188)
        {
            return NewResponse<GSFGetPlayerQuestsByQuestIdsSvc.GSFResponse>(header);
        }

        if (header.msgType == 563)
        {
            return NewResponse<GSFGetInvitedPlayerQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 564)
        {
            return NewResponse<GSFAcceptQuestTransactionSvc.GSFResponse>(header);
        }

        if (header.msgType == 565)
        {
            return NewResponse<GSFFinalizeQuestTransactionSvc.GSFResponse>(header);
        }

        if (header.msgType == 557)
        {
            return NewResponse<GSFManageSynchronizedObjectsSvc.GSFResponse>(header);
        }

        if (header.msgType == 93)
        {
            return NewResponse<GSFConsumeInventoryItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 94)
        {
            return NewResponse<GSFMoveInventoryItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 95)
        {
            return NewResponse<GSFSwapInventoryItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 77)
        {
            return NewResponse<GSFAddCollectionItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 78)
        {
            return NewResponse<GSFDropCollectionItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 79)
        {
            return NewResponse<GSFResetCollectionSvc.GSFResponse>(header);
        }

        if (header.msgType == 76)
        {
            return NewResponse<GSFGetCmsCollectionItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 88)
        {
            return NewResponse<GSFSendPrivateChatGroupInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 89)
        {
            return NewResponse<GSFSendMessageSvc.GSFResponse>(header);
        }

        if (header.msgType == 104)
        {
            return NewResponse<GSFStartPrivateChatGroupSvc.GSFResponse>(header);
        }

        if (header.msgType == 105)
        {
            return NewResponse<GSFAcceptPrivateChatGroupInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 106)
        {
            return NewResponse<GSFLeavePrivateGroupSvc.GSFResponse>(header);
        }

        if (header.msgType == 116)
        {
            return NewResponse<GSFRejoinPrivateChatGroupSvc.GSFResponse>(header);
        }

        if (header.msgType == 107)
        {
            return NewResponse<GSFGetChatChannelTypesSvc.GSFResponse>(header);
        }

        if (header.msgType == 118)
        {
            return NewResponse<GSFGetPlayerChatHistorySvc.GSFResponse>(header);
        }

        if (header.msgType == 99)
        {
            return NewResponse<GSFPlaceYardItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 98)
        {
            return NewResponse<GSFGetYardItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 44)
        {
            return NewResponse<GSFPlaceMazeItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 97)
        {
            return NewResponse<GSFGetMazeItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 519)
        {
            return NewResponse<GSFAttachSingleItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 488)
        {
            return NewResponse<GSFAttachAndPlaceItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 487)
        {
            return NewResponse<GSFRemoveAndDetachMazeItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 447)
        {
            return NewResponse<GSFDetachItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 100)
        {
            return NewResponse<GSFRemoveYardItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 45)
        {
            return NewResponse<GSFRemoveMazeItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 27)
        {
            return NewResponse<GSFInitLocationSvc.GSFResponse>(header);
        }

        if (header.msgType == 154)
        {
            return NewResponse<GSFGetZonesSvc.GSFResponse>(header);
        }

        if (header.msgType == 342)
        {
            return NewResponse<GSFGetAllZonesSvc.GSFResponse>(header);
        }

        if (header.msgType == 413)
        {
            return NewResponse<GSFGetAllBuildingsSvc.GSFResponse>(header);
        }

        if (header.msgType == 137)
        {
            return NewResponse<GSFGetItemCategoriesSvc.GSFResponse>(header);
        }

        if (header.msgType == 178)
        {
            return NewResponse<GSFGetStoreThemesSvc.GSFResponse>(header);
        }

        if (header.msgType == 156)
        {
            return NewResponse<GSFListStoreInventorySvc.GSFResponse>(header);
        }

        if (header.msgType == 206)
        {
            return NewResponse<GSFListStoreInventoryItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 158)
        {
            return NewResponse<GSFPurchaseItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 567)
        {
            return NewResponse<GSFPurchaseWalletItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 180)
        {
            return NewResponse<GSFGetCurrenciesSvc.GSFResponse>(header);
        }

        if (header.msgType == 297)
        {
            return NewResponse<GSFGetStoreItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 344)
        {
            return NewResponse<GSFGetAllStoreItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 412)
        {
            return NewResponse<GSFSellItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 337)
        {
            return NewResponse<GSFEnterStoreSvc.GSFResponse>(header);
        }

        if (header.msgType == 338)
        {
            return NewResponse<GSFExitStoreSvc.GSFResponse>(header);
        }

        if (header.msgType == 29)
        {
            return NewResponse<GSFEnterBuildingSvc.GSFResponse>(header);
        }

        if (header.msgType == 147)
        {
            return NewResponse<GSFGetAnnouncementsSvc.GSFResponse>(header);
        }

        if (header.msgType == 145)
        {
            return NewResponse<GSFGetPlayerReceivedGiftsSvc.GSFResponse>(header);
        }

        if (header.msgType == 140)
        {
            return NewResponse<GSFGetPlayerGiftsSvc.GSFResponse>(header);
        }

        if (header.msgType == 146)
        {
            return NewResponse<GSFManageGiftRequestSvc.GSFResponse>(header);
        }

        if (header.msgType == 153)
        {
            return NewResponse<GSFFindGiftByPlayerGiftIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 55)
        {
            return NewResponse<GSFClaimDynamicSurpriseSvc.GSFResponse>(header);
        }

        if (header.msgType == 193)
        {
            return NewResponse<GSFGetPlayerAvatarsByBirthdaySvc.GSFResponse>(header);
        }

        if (header.msgType == 2)
        {
            return NewResponse<GSFClaimGiftSvc.GSFResponse>(header);
        }

        if (header.msgType == 176)
        {
            return NewResponse<GSFUndressAvatarSvc.GSFResponse>(header);
        }

        if (header.msgType == 110)
        {
            return NewResponse<GSFDressAvatarSvc.GSFResponse>(header);
        }

        if (header.msgType == 174)
        {
            return NewResponse<GSFDressAvatarItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 111)
        {
            return NewResponse<GSFGetOutfitsSvc.GSFResponse>(header);
        }

        if (header.msgType == 103)
        {
            return NewResponse<GSFGetOutfitItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 102)
        {
            return NewResponse<GSFGetAvatarItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 182)
        {
            return NewResponse<GSFAddOutfitSvc.GSFResponse>(header);
        }

        if (header.msgType == 181)
        {
            return NewResponse<GSFAddOutfitItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 184)
        {
            return NewResponse<GSFRemoveOutfitSvc.GSFResponse>(header);
        }

        if (header.msgType == 183)
        {
            return NewResponse<GSFRemoveOutfitItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 185)
        {
            return NewResponse<GSFReplaceOutfitItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 186)
        {
            return NewResponse<GSFSetCurrentOutfitSvc.GSFResponse>(header);
        }

        if (header.msgType == 187)
        {
            return NewResponse<GSFGetFriendAvatarsSvc.GSFResponse>(header);
        }

        if (header.msgType == 177)
        {
            return NewResponse<GSFGetMazePiecesByPlayerMazeIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 173)
        {
            return NewResponse<GSFGetNpcsSvc.GSFResponse>(header);
        }

        if (header.msgType == 171)
        {
            return NewResponse<GSFGetNpcRelationshipsSvc.GSFResponse>(header);
        }

        if (header.msgType == 16)
        {
            return NewResponse<GSFGetPlayerNpcsSvc.GSFResponse>(header);
        }

        if (header.msgType == 169)
        {
            return NewResponse<GSFNpcInteractionSvc.GSFResponse>(header);
        }

        if (header.msgType == 127)
        {
            return NewResponse<GSFGetQuestByIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 207)
        {
            return NewResponse<GSFGetOtherPlayerDetailsSvc.GSFResponse>(header);
        }

        if (header.msgType == 445)
        {
            return NewResponse<GSFGetOtherPlayerDetailsListSvc.GSFResponse>(header);
        }

        if (header.msgType == 295)
        {
            return NewResponse<GSFGetNpcWithMostRelationshipSvc.GSFResponse>(header);
        }

        if (header.msgType == 329)
        {
            return NewResponse<GSFGetNpcsWithQuestOfferSvc.GSFResponse>(header);
        }

        if (header.msgType == 343)
        {
            return NewResponse<GSFGetAllNpcsSvc.GSFResponse>(header);
        }

        if (header.msgType == 417)
        {
            return NewResponse<GSFGetNpcRelationshipLevelsSvc.GSFResponse>(header);
        }

        if (header.msgType == 490)
        {
            return NewResponse<GSFGetQuestFromParentSvc.GSFResponse>(header);
        }

        if (header.msgType == 546)
        {
            return NewResponse<GSFGetQuestAllFromParentSvc.GSFResponse>(header);
        }

        if (header.msgType == 191)
        {
            return NewResponse<GSFManageHomeInvitationsSvc.GSFResponse>(header);
        }

        if (header.msgType == 190)
        {
            return NewResponse<GSFGetHomeInvitationsSvc.GSFResponse>(header);
        }

        if (header.msgType == 328)
        {
            return NewResponse<GSFGetDebugQuestsSvc.GSFResponse>(header);
        }

        if (header.msgType == 334)
        {
            return NewResponse<GSFDebugCreateQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 112)
        {
            return NewResponse<GSFStartQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 52)
        {
            return NewResponse<GSFAddQuestItemSvc.GSFResponse>(header);
        }

        if (header.msgType == 51)
        {
            return NewResponse<GSFCompleteQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 218)
        {
            return NewResponse<GSFAbandonQuestSvc.GSFResponse>(header);
        }

        if (header.msgType == 57)
        {
            return NewResponse<GSFGetNotificationByPlayerNotificationIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 71)
        {
            return NewResponse<GSFAcknowledgeNotificationSvc.GSFResponse>(header);
        }

        if (header.msgType == 74)
        {
            return NewResponse<GSFClearNotificationsByPlayerIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 58)
        {
            return NewResponse<GSFGetNotificationByPlayerIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 245)
        {
            return NewResponse<GSFGetCmsNotificationsSvc.GSFResponse>(header);
        }

        if (header.msgType == 205)
        {
            return NewResponse<GSFGetPlayerStatsSvc.GSFResponse>(header);
        }

        if (header.msgType == 213)
        {
            return NewResponse<GSFGetRequiredExperienceSvc.GSFResponse>(header);
        }

        if (header.msgType == 209)
        {
            return NewResponse<GSFGetStatsTypeSvc.GSFResponse>(header);
        }

        if (header.msgType == 113)
        {
            return NewResponse<GSFHeartbeatSvc.GSFResponse>(header);
        }

        if (header.msgType == 321)
        {
            return NewResponse<GSFSavePlayerSettingsSvc.GSFResponse>(header);
        }

        if (header.msgType == 354)
        {
            return NewResponse<GSFGetPlayerCrispDataSvc.GSFResponse>(header);
        }

        if (header.msgType == 299)
        {
            return NewResponse<GSFGetCrispActionsSvc.GSFResponse>(header);
        }

        if (header.msgType == 359)
        {
            return NewResponse<GSFGetCrispActionsForNonLoggedInSessionSvc.GSFResponse>(header);
        }

        if (header.msgType == 327)
        {
            return NewResponse<GSFGetTiersSvc.GSFResponse>(header);
        }

        if (header.msgType == 194)
        {
            return NewResponse<GSFSendQuestInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 119)
        {
            return NewResponse<GSFGetQuestItemsSvc.GSFResponse>(header);
        }

        if (header.msgType == 138)
        {
            return NewResponse<GSFNextCaptchaSvc.GSFResponse>(header);
        }

        if (header.msgType == 139)
        {
            return NewResponse<GSFCheckCaptchaSvc.GSFResponse>(header);
        }

        if (header.msgType == 40)
        {
            return NewResponse<GSFGetRandomWorldNameSvc.GSFResponse>(header);
        }

        if (header.msgType == 541)
        {
            return NewResponse<GSFGetRandomNamesSvc.GSFResponse>(header);
        }

        if (header.msgType == 358)
        {
            return NewResponse<GSFGetSCSInvalidCodeStatusSvc.GSFResponse>(header);
        }

        if (header.msgType == 544)
        {
            return NewResponse<GSFQuestEventInProgressSvc.GSFResponse>(header);
        }

        if (header.msgType == 157)
        {
            return NewResponse<GSFListStoresSvc.GSFResponse>(header);
        }

        if (header.msgType == 332)
        {
            return NewResponse<GSFGetPlayerMissionsSvc.GSFResponse>(header);
        }

        if (header.msgType == 305)
        {
            return NewResponse<GSFGetCmsMissionsSvc.GSFResponse>(header);
        }

        if (header.msgType == 331)
        {
            return NewResponse<GSFGetPlayerMissionDetailSvc.GSFResponse>(header);
        }

        if (header.msgType == 330)
        {
            return NewResponse<GSFCreatePlayerMissionSvc.GSFResponse>(header);
        }

        if (header.msgType == 306)
        {
            return NewResponse<GSFGetRuleInfoSvc.GSFResponse>(header);
        }

        if (header.msgType == 443)
        {
            return NewResponse<GSFGetRuleInfoListSvc.GSFResponse>(header);
        }

        if (header.msgType == 318)
        {
            return NewResponse<GSFGetPendingEventsSvc.GSFResponse>(header);
        }

        if (header.msgType == 315)
        {
            return NewResponse<GSFGetCmsEventsSvc.GSFResponse>(header);
        }

        if (header.msgType == 317)
        {
            return NewResponse<GSFRequestEventSvc.GSFResponse>(header);
        }

        if (header.msgType == 316)
        {
            return NewResponse<GSFAddEventSvc.GSFResponse>(header);
        }

        if (header.msgType == 319)
        {
            return NewResponse<GSFAcceptEventSvc.GSFResponse>(header);
        }

        if (header.msgType == 320)
        {
            return NewResponse<GSFRejectEventSvc.GSFResponse>(header);
        }

        if (header.msgType == 201)
        {
            return NewResponse<GSFGetPlayerReceivedQuestInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 199)
        {
            return NewResponse<GSFAcceptQuestInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 200)
        {
            return NewResponse<GSFDeclineQuestInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 202)
        {
            return NewResponse<GSFGetPlayerQuestInviteSvc.GSFResponse>(header);
        }

        if (header.msgType == 380)
        {
            return NewResponse<GSFGetRuleInstanceInfoSvc.GSFResponse>(header);
        }

        if (header.msgType == 444)
        {
            return NewResponse<GSFGetRuleInstanceInfoListSvc.GSFResponse>(header);
        }

        if (header.msgType == 436)
        {
            return NewResponse<GSFGetRuleCountSvc.GSFResponse>(header);
        }

        if (header.msgType == 511)
        {
            return NewResponse<GSFGetRuleCountListSvc.GSFResponse>(header);
        }

        if (header.msgType == 494)
        {
            return NewResponse<GSFGetRuleInstanceDataSvc.GSFResponse>(header);
        }

        if (header.msgType == 179)
        {
            return NewResponse<GSFGetAssetsByOIDsSvc.GSFResponse>(header);
        }

        if (header.msgType == 132)
        {
            return NewResponse<GSFGetGamesSvc.GSFResponse>(header);
        }

        if (header.msgType == 189)
        {
            return NewResponse<GSFGetZoneGamesSvc.GSFResponse>(header);
        }

        if (header.msgType == 120)
        {
            return NewResponse<GSFStartGameSvc.GSFResponse>(header);
        }

        if (header.msgType == 53)
        {
            return NewResponse<GSFEndGameSvc.GSFResponse>(header);
        }

        if (header.msgType == 373)
        {
            return NewResponse<GSFReportAbuseSvc.GSFResponse>(header);
        }

        if (header.msgType == 159)
        {
            return NewResponse<GSFCompleteTutorialSvc.GSFResponse>(header);
        }

        if (header.msgType == 498)
        {
            return NewResponse<GSFProcessCrispMessageSvc.GSFResponse>(header);
        }

        if (header.msgType == 523)
        {
            return NewResponse<GSFMarkAnnouncementReadSvc.GSFResponse>(header);
        }

        if (header.msgType == 522)
        {
            return NewResponse<GSFGetStatefulInstanceSvc.GSFResponse>(header);
        }

        if (header.msgType == 527)
        {
            return NewResponse<GSFGetPlayerExternalSiteMapSvc.GSFResponse>(header);
        }

        if (header.msgType == 528)
        {
            return NewResponse<GSFSetPlayerExternalSiteMapSvc.GSFResponse>(header);
        }

        if (header.msgType == 542)
        {
            return NewResponse<GSFSelectedPlayerNameSvc.GSFResponse>(header);
        }

        if (header.msgType == 335)
        {
            return NewResponse<GSFRegisterAvatarForRegistrationSvc.GSFResponse>(header);
        }

        if (header.msgType == 566)
        {
            return new GSFResponseMessage(header, typeof(GSFGetClientVersionInfoSvc.GSFResponse));
        }

        if (header.msgType == 568)
        {
            return NewResponse<GSFGetPlayerOnlineStatusSvc.GSFResponse>(header);
        }

        if (header.msgType == 549)
        {
            return NewResponse<GSFVoteOnPlayerVotedSvc.GSFResponse>(header);
        }

        if (header.msgType == 550)
        {
            return NewResponse<GSFGetPlayerVotedSvc.GSFResponse>(header);
        }

        if (header.msgType == 548)
        {
            return NewResponse<GSFCreatePlayerVotedSvc.GSFResponse>(header);
        }

        if (header.msgType == 556)
        {
            return NewResponse<GSFGetFriendsPlayerVotedsSvc.GSFResponse>(header);
        }

        if (header.msgType == 547)
        {
            return NewResponse<GSFGetPlayerVotedListSvc.GSFResponse>(header);
        }

        if (header.msgType == 553)
        {
            return NewResponse<GSFCompletePlayerVotedSvc.GSFResponse>(header);
        }

        if (header.msgType == 569)
        {
            return NewResponse<GSFGetItemByIdSvc.GSFResponse>(header);
        }

        if (header.msgType == 313)
        {
            return NewResponse<GSFSellItemInfoSvc.GSFResponse>(header);
        }

        if (header.msgType == 571)
        {
            return NewResponse<GSFGetPublicItemsByOIDsSvc.GSFResponse>(header);
        }

        if (header.msgType == 572)
        {
            return NewResponse<GSFGetPublicItemCategoriesSvc.GSFResponse>(header);
        }

        if (header.msgType == 574)
        {
            return NewResponse<GSFCreateRecipeSvc.GSFResponse>(header);
        }

        if (header.msgType == 575)
        {
            return NewResponse<GSFGetNpcsByChildHierarchiesSvc.GSFResponse>(header);
        }

        if (header.msgType == 577)
        {
            return NewResponse<GSFEnhanceRecipeSvc.GSFResponse>(header);
        }

        if (header.msgType == 576)
        {
            return NewResponse<GSFRecycleRecipeSvc.GSFResponse>(header);
        }

        if (header.msgType == 570)
        {
            return NewResponse<GSFGetPublicAssetsByOIDsSvc.GSFResponse>(header);
        }

        Console.WriteLine("Unknown user message type: " + header.msgType);
        return null;
    }

    private GSFMessage BuildClientMessage(MessageHeader header)
    {
        switch (header.msgType)
        {
            case 4:
                return new GSFChangeObjectNotify();
            case 7:
                return new GSFRemovePlayerNotify();
            case 20:
                return new GSFAddPlayerNotify();
            case 6:
                return new GSFPlayerMoveNotify();
            case 23:
                return new GSFMinimapNotify();
            case 24:
                return new GSFPositionRecapNotify();
            case 19:
                return new GSFInterestListNotify();
            case 12:
                return new GSFChangeWeightNotify();
            case 11:
                return new GSFPlayerEmoteNotify();
            case 8:
                return new GSFChatNotify();
            case 21:
                return new GSFUpdateNpcsNotify();
            case 22:
                return new GSFStopNpcNotify();
            case 16:
                return new GSFNotificationNotify();
            case 25:
                return new GSFPlayerActionNotify();
            case 26:
                return new GSFEvictNotify();
            case 1:
                return new GSFAddObjectNotify();
            case 3:
                return new GSFRemoveObjectNotify();
            default:
                Console.WriteLine("Unknown notify message type: " + header.msgType);
                return null;
        }
    }

    private GSFMessage BuildSyncServerMessage(MessageHeader header)
    {
        switch (header.msgType)
        {
            case 33:
                return NewResponse<GSFSyncLoginSvc.GSFResponse>(header);
            case 48:
                return NewResponse<GSFSyncLogoutSvc.GSFResponse>(header);
            case 35:
                return NewResponse<GSFUpdateUserFriendsSvc.GSFResponse>(header);
            case 55:
                return NewResponse<GSFSyncReLoginSvc.GSFResponse>(header);
            default:
                Console.WriteLine("Unknown sync message type: " + header.msgType);
                return null;
        }
    }

    private ServerResponseMessage NewResponse<T>(MessageHeader header) where T : GSFService.GSFResponse
    {
        return new ServerResponseMessage(header, typeof(T));
    }
}
