export const API_ENDPOINTS = {
    addFriendUrl: "api/Friend/AddFriend",
    addMeetupUrl: "api/Meetup/AddMeetup",
    addSpeakerUrl: "api/Speaker/AddSpeaker",
    addTalkUrl: "api/Talk/AddTalk",
    addVenueUrl: "api/Venue/AddVenue",
    getFriendUrl: "api/Friend/GetFriend/{{friendId}}",
    getFriendsUrl: "api/Friend/GetFriends",
    getMeetupUrl: "api/Meetup/GetMeetup/{{meetupId}}",
    getMeetupsUrl: "api/Meetup/GetMeetups",
    getSpeakerUrl: "api/Speaker/GetSpeaker/{{speakerId}}",
    getSpeakersUrl: "api/Speaker/GetSpeakers",
    getTalkUrl: "api/Talk/GetTalk/{{talkId}}",
    getTalksUrl: "api/Talk/GetTalks",
    getVenueUrl: "api/Venue/GetVenue/{{venueId}}",
    getVenuesUrl: "api/Venue/GetVenues",
    storeFriendAvatarUrl: "api/File/StoreFriendAvatar/{{friendId}}",
    storeSpeakerAvatarUrl: "api/File/StoreSpeakerAvatar/{{speakerId}}",
    updateFriendUrl: "api/Friend/UpdateFriend",
    updateMeetupUrl: "api/Meetup/UpdateMeetup",
    updateSpeakerUrl: "api/Speaker/UpdateSpeaker",
    updateTalkUrl: "api/Talk/UpdateTalk",
    updateVenueUrl: "api/Venue/UpdateVenue",
};

export const LABELS = {
    ADDRESS: "Адрес",
    BLOG_URL: "Ссылка на блог",
    CODE_URL: "Ссылка на код",
    COMPANY: "Компания",
    COMPANY_URL: "Сайт компании",
    CONTACTS_URL: "Ссылка на контакты",
    DESCRIPTION: "Описание",
    GIT_HUB_URL: "Ссылка на GitHub",
    HABR_URL: "Ссылка на хабр",
    IDENTITY: "Идентификатор",
    MAP_URL: "Ссылка на карту",
    NAME: "Имя",
    SLIDES_URL: "Ссылка на слайды",
    TITLE: "Название",
    TWITTER_URL: "Ссылка на твиттер",
    URL: "Ссылка",
    VIDEO_URL: "Ссылка на видео",
};

export const PATTERNS = {
    IDENTITY: "^[A-Z][\\w-]{2,}$",
    REQUIRED_STRING: "^\\S[\\s\\S]*\\S$",
    URI: "^https?://.+\\..+$",
};

export const MIME_TYPES = {
    JPEG: "image/jpeg",
    PNG: "image/png",
};

export const FILE_SIZES = {
    AVATAR_MAX_SIZE: 40000,
};
