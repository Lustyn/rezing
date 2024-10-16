using System;

public class ServerMessageFactory : GSFBitProtocolCodec.IMessageFactory
{
    public GSFMessage BuildMessage(MessageHeader header)
    {
        Console.WriteLine($"Building message with svcClass: {header.svcClass}, ${header.msgType}, ${header}");
        switch (header.svcClass)
		{
            case 18:
                return BuildRequestMessage(header);
            case 19:
                throw new NotImplementedException(); // Sync msgs?
            case -1:
                throw new NotImplementedException(); // Client msgs?
            default:
                throw new Exception("Invalid svcClass");
		}
    }

    private GSFMessage BuildRequestMessage(MessageHeader header)
    {
        if (header.msgType == 15)
		{
			return NewRequest<GSFLoginSvc.GSFRequest>(header);
		}
		if (header.msgType == 26)
		{
			return NewRequest<GSFReLoginSvc.GSFRequest>(header);
		}
		if (header.msgType == 28)
		{
			return NewRequest<GSFGetLangLocaleSvc.GSFRequest>(header);
		}
		if (header.msgType == 19)
		{
			return NewRequest<GSFLogoutSvc.GSFRequest>(header);
		}
		if (header.msgType == 136)
		{
			return NewRequest<GSFGetSiteFrameSvc.GSFRequest>(header);
		}
		if (header.msgType == 3)
		{
			return NewRequest<GSFRegisterPlayerSvc.GSFRequest>(header);
		}
		if (header.msgType == 18)
		{
			return NewRequest<GSFRegisterFCSSCodeSvc.GSFRequest>(header);
		}
		if (header.msgType == 23)
		{
			return NewRequest<GSFUpdateAvatarNameSvc.GSFRequest>(header);
		}
		if (header.msgType == 1)
		{
			return NewRequest<GSFGetAvatarsSvc.GSFRequest>(header);
		}
		if (header.msgType == 135)
		{
			return NewRequest<GSFUpdatePlayerActiveAvatarSvc.GSFRequest>(header);
		}
		if (header.msgType == 17)
		{
			return NewRequest<GSFCheckUsernameSvc.GSFRequest>(header);
		}
		if (header.msgType == 543)
		{
			return NewRequest<GSFValidateNameSvc.GSFRequest>(header);
		}
		if (header.msgType == 481)
		{
			return NewRequest<GSFPreFilterNameCheckAvailabilitySvc.GSFRequest>(header);
		}
		if (header.msgType == 20)
		{
			return NewRequest<GSFGetInventoryObjectsSvc.GSFRequest>(header);
		}
		if (header.msgType == 21)
		{
			return NewRequest<GSFGetBuildObjectsSvc.GSFRequest>(header);
		}
		if (header.msgType == 22)
		{
			return NewRequest<GSFGetCollectionObjectsSvc.GSFRequest>(header);
		}
		if (header.msgType == 5)
		{
			return NewRequest<GSFStartMazeEditSvc.GSFRequest>(header);
		}
		if (header.msgType == 8)
		{
			return NewRequest<GSFEndMazeEditSvc.GSFRequest>(header);
		}
		if (header.msgType == 9)
		{
			return NewRequest<GSFDeleteMazeSvc.GSFRequest>(header);
		}
		if (header.msgType == 62)
		{
			return NewRequest<GSFGetSystemMazeRatingSvc.GSFRequest>(header);
		}
		if (header.msgType == 108)
		{
			return NewRequest<GSFStartMazePlaySvc.GSFRequest>(header);
		}
		if (header.msgType == 109)
		{
			return NewRequest<GSFEndMazePlaySvc.GSFRequest>(header);
		}
		if (header.msgType == 67)
		{
			return NewRequest<GSFGetHomeMazeSvc.GSFRequest>(header);
		}
		if (header.msgType == 38)
		{
			return NewRequest<GSFAddFriendSvc.GSFRequest>(header);
		}
		if (header.msgType == 192)
		{
			return NewRequest<GSFFindPlayerByNicknameSvc.GSFRequest>(header);
		}
		if (header.msgType == 84)
		{
			return NewRequest<GSFGetFriendByFriendPlayerIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 82)
		{
			return NewRequest<GSFGetActiveFriendListSvc.GSFRequest>(header);
		}
		if (header.msgType == 36)
		{
			return NewRequest<GSFGetBlockedPlayersSvc.GSFRequest>(header);
		}
		if (header.msgType == 33)
		{
			return NewRequest<GSFGetFriendListSvc.GSFRequest>(header);
		}
		if (header.msgType == 409)
		{
			return NewRequest<GSFSetPlayerFindableSvc.GSFRequest>(header);
		}
		if (header.msgType == 396)
		{
			return NewRequest<GSFListLimitsSvc.GSFRequest>(header);
		}
		if (header.msgType == 324)
		{
			return NewRequest<GSFSuggestFriendsSvc.GSFRequest>(header);
		}
		if (header.msgType == 455)
		{
			return NewRequest<GSFListFindablePlayersSvc.GSFRequest>(header);
		}
		if (header.msgType == 83)
		{
			return NewRequest<GSFGetFriendRequestsSvc.GSFRequest>(header);
		}
		if (header.msgType == 459)
		{
			return NewRequest<GSFGetFriendshipRequestCountsSvc.GSFRequest>(header);
		}
		if (header.msgType == 81)
		{
			return NewRequest<GSFUpdateFriendCommentSvc.GSFRequest>(header);
		}
		if (header.msgType == 37)
		{
			return NewRequest<GSFManageBlockPlayerSvc.GSFRequest>(header);
		}
		if (header.msgType == 34)
		{
			return NewRequest<GSFRemoveFriendSvc.GSFRequest>(header);
		}
		if (header.msgType == 35)
		{
			return NewRequest<GSFManageFriendRequestSvc.GSFRequest>(header);
		}
		if (header.msgType == 7)
		{
			return NewRequest<GSFGetCommunityMazeSvc.GSFRequest>(header);
		}
		if (header.msgType == 49)
		{
			return NewRequest<GSFGetCommunityMazeThumbnailsSvc.GSFRequest>(header);
		}
		if (header.msgType == 12)
		{
			return NewRequest<GSFGetCommunityMazesSvc.GSFRequest>(header);
		}
		if (header.msgType == 92)
		{
			return NewRequest<GSFGetPlayerMazePlaySvc.GSFRequest>(header);
		}
		if (header.msgType == 101)
		{
			return NewRequest<GSFGetSystemMazePlaySvc.GSFRequest>(header);
		}
		if (header.msgType == 80)
		{
			return NewRequest<GSFApproveMazePublishingSvc.GSFRequest>(header);
		}
		if (header.msgType == 248)
		{
			return NewRequest<GSFGetOnlineStatusesSvc.GSFRequest>(header);
		}
		if (header.msgType == 6)
		{
			return NewRequest<GSFGetPlayerMazeSvc.GSFRequest>(header);
		}
		if (header.msgType == 4)
		{
			return NewRequest<GSFGetPlayerMazesSvc.GSFRequest>(header);
		}
		if (header.msgType == 13)
		{
			return NewRequest<GSFGetQuestMazesSvc.GSFRequest>(header);
		}
		if (header.msgType == 11)
		{
			return NewRequest<GSFGetZoneMazesSvc.GSFRequest>(header);
		}
		if (header.msgType == 63)
		{
			return NewRequest<GSFRatePlayerMazeSvc.GSFRequest>(header);
		}
		if (header.msgType == 50)
		{
			return NewRequest<GSFAcceptQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 491)
		{
			return NewRequest<GSFCreateQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 168)
		{
			return NewRequest<GSFGetPlayerQuestsSvc.GSFRequest>(header);
		}
		if (header.msgType == 188)
		{
			return NewRequest<GSFGetPlayerQuestsByQuestIdsSvc.GSFRequest>(header);
		}
		if (header.msgType == 563)
		{
			return NewRequest<GSFGetInvitedPlayerQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 564)
		{
			return NewRequest<GSFAcceptQuestTransactionSvc.GSFRequest>(header);
		}
		if (header.msgType == 565)
		{
			return NewRequest<GSFFinalizeQuestTransactionSvc.GSFRequest>(header);
		}
		if (header.msgType == 557)
		{
			return NewRequest<GSFManageSynchronizedObjectsSvc.GSFRequest>(header);
		}
		if (header.msgType == 93)
		{
			return NewRequest<GSFConsumeInventoryItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 94)
		{
			return NewRequest<GSFMoveInventoryItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 95)
		{
			return NewRequest<GSFSwapInventoryItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 77)
		{
			return NewRequest<GSFAddCollectionItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 78)
		{
			return NewRequest<GSFDropCollectionItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 79)
		{
			return NewRequest<GSFResetCollectionSvc.GSFRequest>(header);
		}
		if (header.msgType == 76)
		{
			return NewRequest<GSFGetCmsCollectionItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 88)
		{
			return NewRequest<GSFSendPrivateChatGroupInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 89)
		{
			return NewRequest<GSFSendMessageSvc.GSFRequest>(header);
		}
		if (header.msgType == 104)
		{
			return NewRequest<GSFStartPrivateChatGroupSvc.GSFRequest>(header);
		}
		if (header.msgType == 105)
		{
			return NewRequest<GSFAcceptPrivateChatGroupInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 106)
		{
			return NewRequest<GSFLeavePrivateGroupSvc.GSFRequest>(header);
		}
		if (header.msgType == 116)
		{
			return NewRequest<GSFRejoinPrivateChatGroupSvc.GSFRequest>(header);
		}
		if (header.msgType == 107)
		{
			return NewRequest<GSFGetChatChannelTypesSvc.GSFRequest>(header);
		}
		if (header.msgType == 118)
		{
			return NewRequest<GSFGetPlayerChatHistorySvc.GSFRequest>(header);
		}
		if (header.msgType == 99)
		{
			return NewRequest<GSFPlaceYardItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 98)
		{
			return NewRequest<GSFGetYardItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 44)
		{
			return NewRequest<GSFPlaceMazeItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 97)
		{
			return NewRequest<GSFGetMazeItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 519)
		{
			return NewRequest<GSFAttachSingleItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 488)
		{
			return NewRequest<GSFAttachAndPlaceItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 487)
		{
			return NewRequest<GSFRemoveAndDetachMazeItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 447)
		{
			return NewRequest<GSFDetachItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 100)
		{
			return NewRequest<GSFRemoveYardItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 45)
		{
			return NewRequest<GSFRemoveMazeItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 27)
		{
			return NewRequest<GSFInitLocationSvc.GSFRequest>(header);
		}
		if (header.msgType == 154)
		{
			return NewRequest<GSFGetZonesSvc.GSFRequest>(header);
		}
		if (header.msgType == 342)
		{
			return NewRequest<GSFGetAllZonesSvc.GSFRequest>(header);
		}
		if (header.msgType == 413)
		{
			return NewRequest<GSFGetAllBuildingsSvc.GSFRequest>(header);
		}
		if (header.msgType == 137)
		{
			return NewRequest<GSFGetItemCategoriesSvc.GSFRequest>(header);
		}
		if (header.msgType == 178)
		{
			return NewRequest<GSFGetStoreThemesSvc.GSFRequest>(header);
		}
		if (header.msgType == 156)
		{
			return NewRequest<GSFListStoreInventorySvc.GSFRequest>(header);
		}
		if (header.msgType == 206)
		{
			return NewRequest<GSFListStoreInventoryItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 158)
		{
			return NewRequest<GSFPurchaseItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 567)
		{
			return NewRequest<GSFPurchaseWalletItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 180)
		{
			return NewRequest<GSFGetCurrenciesSvc.GSFRequest>(header);
		}
		if (header.msgType == 297)
		{
			return NewRequest<GSFGetStoreItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 344)
		{
			return NewRequest<GSFGetAllStoreItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 412)
		{
			return NewRequest<GSFSellItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 337)
		{
			return NewRequest<GSFEnterStoreSvc.GSFRequest>(header);
		}
		if (header.msgType == 338)
		{
			return NewRequest<GSFExitStoreSvc.GSFRequest>(header);
		}
		if (header.msgType == 29)
		{
			return NewRequest<GSFEnterBuildingSvc.GSFRequest>(header);
		}
		if (header.msgType == 147)
		{
			return NewRequest<GSFGetAnnouncementsSvc.GSFRequest>(header);
		}
		if (header.msgType == 145)
		{
			return NewRequest<GSFGetPlayerReceivedGiftsSvc.GSFRequest>(header);
		}
		if (header.msgType == 140)
		{
			return NewRequest<GSFGetPlayerGiftsSvc.GSFRequest>(header);
		}
		if (header.msgType == 146)
		{
			return NewRequest<GSFManageGiftRequestSvc.GSFRequest>(header);
		}
		if (header.msgType == 153)
		{
			return NewRequest<GSFFindGiftByPlayerGiftIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 55)
		{
			return NewRequest<GSFClaimDynamicSurpriseSvc.GSFRequest>(header);
		}
		if (header.msgType == 193)
		{
			return NewRequest<GSFGetPlayerAvatarsByBirthdaySvc.GSFRequest>(header);
		}
		if (header.msgType == 2)
		{
			return NewRequest<GSFClaimGiftSvc.GSFRequest>(header);
		}
		if (header.msgType == 176)
		{
			return NewRequest<GSFUndressAvatarSvc.GSFRequest>(header);
		}
		if (header.msgType == 110)
		{
			return NewRequest<GSFDressAvatarSvc.GSFRequest>(header);
		}
		if (header.msgType == 174)
		{
			return NewRequest<GSFDressAvatarItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 111)
		{
			return NewRequest<GSFGetOutfitsSvc.GSFRequest>(header);
		}
		if (header.msgType == 103)
		{
			return NewRequest<GSFGetOutfitItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 102)
		{
			return NewRequest<GSFGetAvatarItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 182)
		{
			return NewRequest<GSFAddOutfitSvc.GSFRequest>(header);
		}
		if (header.msgType == 181)
		{
			return NewRequest<GSFAddOutfitItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 184)
		{
			return NewRequest<GSFRemoveOutfitSvc.GSFRequest>(header);
		}
		if (header.msgType == 183)
		{
			return NewRequest<GSFRemoveOutfitItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 185)
		{
			return NewRequest<GSFReplaceOutfitItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 186)
		{
			return NewRequest<GSFSetCurrentOutfitSvc.GSFRequest>(header);
		}
		if (header.msgType == 187)
		{
			return NewRequest<GSFGetFriendAvatarsSvc.GSFRequest>(header);
		}
		if (header.msgType == 177)
		{
			return NewRequest<GSFGetMazePiecesByPlayerMazeIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 173)
		{
			return NewRequest<GSFGetNpcsSvc.GSFRequest>(header);
		}
		if (header.msgType == 171)
		{
			return NewRequest<GSFGetNpcRelationshipsSvc.GSFRequest>(header);
		}
		if (header.msgType == 16)
		{
			return NewRequest<GSFGetPlayerNpcsSvc.GSFRequest>(header);
		}
		if (header.msgType == 169)
		{
			return NewRequest<GSFNpcInteractionSvc.GSFRequest>(header);
		}
		if (header.msgType == 127)
		{
			return NewRequest<GSFGetQuestByIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 207)
		{
			return NewRequest<GSFGetOtherPlayerDetailsSvc.GSFRequest>(header);
		}
		if (header.msgType == 445)
		{
			return NewRequest<GSFGetOtherPlayerDetailsListSvc.GSFRequest>(header);
		}
		if (header.msgType == 295)
		{
			return NewRequest<GSFGetNpcWithMostRelationshipSvc.GSFRequest>(header);
		}
		if (header.msgType == 329)
		{
			return NewRequest<GSFGetNpcsWithQuestOfferSvc.GSFRequest>(header);
		}
		if (header.msgType == 343)
		{
			return NewRequest<GSFGetAllNpcsSvc.GSFRequest>(header);
		}
		if (header.msgType == 417)
		{
			return NewRequest<GSFGetNpcRelationshipLevelsSvc.GSFRequest>(header);
		}
		if (header.msgType == 490)
		{
			return NewRequest<GSFGetQuestFromParentSvc.GSFRequest>(header);
		}
		if (header.msgType == 546)
		{
			return NewRequest<GSFGetQuestAllFromParentSvc.GSFRequest>(header);
		}
		if (header.msgType == 191)
		{
			return NewRequest<GSFManageHomeInvitationsSvc.GSFRequest>(header);
		}
		if (header.msgType == 190)
		{
			return NewRequest<GSFGetHomeInvitationsSvc.GSFRequest>(header);
		}
		if (header.msgType == 328)
		{
			return NewRequest<GSFGetDebugQuestsSvc.GSFRequest>(header);
		}
		if (header.msgType == 334)
		{
			return NewRequest<GSFDebugCreateQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 112)
		{
			return NewRequest<GSFStartQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 52)
		{
			return NewRequest<GSFAddQuestItemSvc.GSFRequest>(header);
		}
		if (header.msgType == 51)
		{
			return NewRequest<GSFCompleteQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 218)
		{
			return NewRequest<GSFAbandonQuestSvc.GSFRequest>(header);
		}
		if (header.msgType == 57)
		{
			return NewRequest<GSFGetNotificationByPlayerNotificationIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 71)
		{
			return NewRequest<GSFAcknowledgeNotificationSvc.GSFRequest>(header);
		}
		if (header.msgType == 74)
		{
			return NewRequest<GSFClearNotificationsByPlayerIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 58)
		{
			return NewRequest<GSFGetNotificationByPlayerIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 245)
		{
			return NewRequest<GSFGetCmsNotificationsSvc.GSFRequest>(header);
		}
		if (header.msgType == 205)
		{
			return NewRequest<GSFGetPlayerStatsSvc.GSFRequest>(header);
		}
		if (header.msgType == 213)
		{
			return NewRequest<GSFGetRequiredExperienceSvc.GSFRequest>(header);
		}
		if (header.msgType == 209)
		{
			return NewRequest<GSFGetStatsTypeSvc.GSFRequest>(header);
		}
		if (header.msgType == 113)
		{
			return NewRequest<GSFHeartbeatSvc.GSFRequest>(header);
		}
		if (header.msgType == 321)
		{
			return NewRequest<GSFSavePlayerSettingsSvc.GSFRequest>(header);
		}
		if (header.msgType == 354)
		{
			return NewRequest<GSFGetPlayerCrispDataSvc.GSFRequest>(header);
		}
		if (header.msgType == 299)
		{
			return NewRequest<GSFGetCrispActionsSvc.GSFRequest>(header);
		}
		if (header.msgType == 359)
		{
			return NewRequest<GSFGetCrispActionsForNonLoggedInSessionSvc.GSFRequest>(header);
		}
		if (header.msgType == 327)
		{
			return NewRequest<GSFGetTiersSvc.GSFRequest>(header);
		}
		if (header.msgType == 194)
		{
			return NewRequest<GSFSendQuestInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 119)
		{
			return NewRequest<GSFGetQuestItemsSvc.GSFRequest>(header);
		}
		if (header.msgType == 138)
		{
			return NewRequest<GSFNextCaptchaSvc.GSFRequest>(header);
		}
		if (header.msgType == 139)
		{
			return NewRequest<GSFCheckCaptchaSvc.GSFRequest>(header);
		}
		if (header.msgType == 40)
		{
			return NewRequest<GSFGetRandomWorldNameSvc.GSFRequest>(header);
		}
		if (header.msgType == 541)
		{
			return NewRequest<GSFGetRandomNamesSvc.GSFRequest>(header);
		}
		if (header.msgType == 358)
		{
			return NewRequest<GSFGetSCSInvalidCodeStatusSvc.GSFRequest>(header);
		}
		if (header.msgType == 544)
		{
			return NewRequest<GSFQuestEventInProgressSvc.GSFRequest>(header);
		}
		if (header.msgType == 157)
		{
			return NewRequest<GSFListStoresSvc.GSFRequest>(header);
		}
		if (header.msgType == 332)
		{
			return NewRequest<GSFGetPlayerMissionsSvc.GSFRequest>(header);
		}
		if (header.msgType == 305)
		{
			return NewRequest<GSFGetCmsMissionsSvc.GSFRequest>(header);
		}
		if (header.msgType == 331)
		{
			return NewRequest<GSFGetPlayerMissionDetailSvc.GSFRequest>(header);
		}
		if (header.msgType == 330)
		{
			return NewRequest<GSFCreatePlayerMissionSvc.GSFRequest>(header);
		}
		if (header.msgType == 306)
		{
			return NewRequest<GSFGetRuleInfoSvc.GSFRequest>(header);
		}
		if (header.msgType == 443)
		{
			return NewRequest<GSFGetRuleInfoListSvc.GSFRequest>(header);
		}
		if (header.msgType == 318)
		{
			return NewRequest<GSFGetPendingEventsSvc.GSFRequest>(header);
		}
		if (header.msgType == 315)
		{
			return NewRequest<GSFGetCmsEventsSvc.GSFRequest>(header);
		}
		if (header.msgType == 317)
		{
			return NewRequest<GSFRequestEventSvc.GSFRequest>(header);
		}
		if (header.msgType == 316)
		{
			return NewRequest<GSFAddEventSvc.GSFRequest>(header);
		}
		if (header.msgType == 319)
		{
			return NewRequest<GSFAcceptEventSvc.GSFRequest>(header);
		}
		if (header.msgType == 320)
		{
			return NewRequest<GSFRejectEventSvc.GSFRequest>(header);
		}
		if (header.msgType == 201)
		{
			return NewRequest<GSFGetPlayerReceivedQuestInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 199)
		{
			return NewRequest<GSFAcceptQuestInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 200)
		{
			return NewRequest<GSFDeclineQuestInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 202)
		{
			return NewRequest<GSFGetPlayerQuestInviteSvc.GSFRequest>(header);
		}
		if (header.msgType == 380)
		{
			return NewRequest<GSFGetRuleInstanceInfoSvc.GSFRequest>(header);
		}
		if (header.msgType == 444)
		{
			return NewRequest<GSFGetRuleInstanceInfoListSvc.GSFRequest>(header);
		}
		if (header.msgType == 436)
		{
			return NewRequest<GSFGetRuleCountSvc.GSFRequest>(header);
		}
		if (header.msgType == 511)
		{
			return NewRequest<GSFGetRuleCountListSvc.GSFRequest>(header);
		}
		if (header.msgType == 494)
		{
			return NewRequest<GSFGetRuleInstanceDataSvc.GSFRequest>(header);
		}
		if (header.msgType == 179)
		{
			return NewRequest<GSFGetAssetsByOIDsSvc.GSFRequest>(header);
		}
		if (header.msgType == 132)
		{
			return NewRequest<GSFGetGamesSvc.GSFRequest>(header);
		}
		if (header.msgType == 189)
		{
			return NewRequest<GSFGetZoneGamesSvc.GSFRequest>(header);
		}
		if (header.msgType == 120)
		{
			return NewRequest<GSFStartGameSvc.GSFRequest>(header);
		}
		if (header.msgType == 53)
		{
			return NewRequest<GSFEndGameSvc.GSFRequest>(header);
		}
		if (header.msgType == 373)
		{
			return NewRequest<GSFReportAbuseSvc.GSFRequest>(header);
		}
		if (header.msgType == 159)
		{
			return NewRequest<GSFCompleteTutorialSvc.GSFRequest>(header);
		}
		if (header.msgType == 498)
		{
			return NewRequest<GSFProcessCrispMessageSvc.GSFRequest>(header);
		}
		if (header.msgType == 523)
		{
			return NewRequest<GSFMarkAnnouncementReadSvc.GSFRequest>(header);
		}
		if (header.msgType == 522)
		{
			return NewRequest<GSFGetStatefulInstanceSvc.GSFRequest>(header);
		}
		if (header.msgType == 527)
		{
			return NewRequest<GSFGetPlayerExternalSiteMapSvc.GSFRequest>(header);
		}
		if (header.msgType == 528)
		{
			return NewRequest<GSFSetPlayerExternalSiteMapSvc.GSFRequest>(header);
		}
		if (header.msgType == 542)
		{
			return NewRequest<GSFSelectedPlayerNameSvc.GSFRequest>(header);
		}
		if (header.msgType == 335)
		{
			return NewRequest<GSFRegisterAvatarForRegistrationSvc.GSFRequest>(header);
		}
		if (header.msgType == 566)
		{
            return NewRequest<GSFGetClientVersionInfoSvc.GSFRequest>(header);
		}
		if (header.msgType == 568)
		{
			return NewRequest<GSFGetPlayerOnlineStatusSvc.GSFRequest>(header);
		}
		if (header.msgType == 549)
		{
			return NewRequest<GSFVoteOnPlayerVotedSvc.GSFRequest>(header);
		}
		if (header.msgType == 550)
		{
			return NewRequest<GSFGetPlayerVotedSvc.GSFRequest>(header);
		}
		if (header.msgType == 548)
		{
			return NewRequest<GSFCreatePlayerVotedSvc.GSFRequest>(header);
		}
		if (header.msgType == 556)
		{
			return NewRequest<GSFGetFriendsPlayerVotedsSvc.GSFRequest>(header);
		}
		if (header.msgType == 547)
		{
			return NewRequest<GSFGetPlayerVotedListSvc.GSFRequest>(header);
		}
		if (header.msgType == 553)
		{
			return NewRequest<GSFCompletePlayerVotedSvc.GSFRequest>(header);
		}
		if (header.msgType == 569)
		{
			return NewRequest<GSFGetItemByIdSvc.GSFRequest>(header);
		}
		if (header.msgType == 313)
		{
			return NewRequest<GSFSellItemInfoSvc.GSFRequest>(header);
		}
		if (header.msgType == 571)
		{
			return NewRequest<GSFGetPublicItemsByOIDsSvc.GSFRequest>(header);
		}
		if (header.msgType == 572)
		{
			return NewRequest<GSFGetPublicItemCategoriesSvc.GSFRequest>(header);
		}
		if (header.msgType == 574)
		{
			return NewRequest<GSFCreateRecipeSvc.GSFRequest>(header);
		}
		if (header.msgType == 575)
		{
			return NewRequest<GSFGetNpcsByChildHierarchiesSvc.GSFRequest>(header);
		}
		if (header.msgType == 577)
		{
			return NewRequest<GSFEnhanceRecipeSvc.GSFRequest>(header);
		}
		if (header.msgType == 576)
		{
			return NewRequest<GSFRecycleRecipeSvc.GSFRequest>(header);
		}
		if (header.msgType == 570)
		{
			return NewRequest<GSFGetPublicAssetsByOIDsSvc.GSFRequest>(header);
		}

		throw new Exception("Invalid msgType");
    }

    private ServerRequestMessage NewRequest<T>(MessageHeader header) where T : GSFService.GSFRequest
	{
		return new ServerRequestMessage(header, typeof(T));
	}
}
