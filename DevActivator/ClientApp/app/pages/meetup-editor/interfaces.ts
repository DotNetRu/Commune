export interface IFriendReference {
    friendId: string;
}

export interface IApiSession {
    talkId: string;
    startTime: string;
    endTime: string;
}

export type ISession = IApiSession & {
    // todo: date to moment
    startTime: Date;
    endTime: Date;
};

export interface IApiMeetup {
    id: string;
    name: string;
    communityId: string;
    friendIds: IFriendReference[];
    venueId: string;
    sessions: IApiSession[];
}

export type IMeetup = IApiMeetup & {
    sessions: ISession[];
};
