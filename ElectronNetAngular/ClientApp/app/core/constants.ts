export const API_ENDPOINTS = {
    addSpeakerUrl: "api/Speaker/AddSpeaker",
    addTalkUrl: "api/Talk/AddTalk",
    getSpeakerUrl: "api/Speaker/GetSpeaker/{{speakerId}}",
    getSpeakersUrl: "api/Speaker/GetSpeakers",
    getTalkUrl: "api/Talk/GetTalk/{{talkId}}",
    getTalksUrl: "api/Talk/GetTalks",
    storeSpeakerAvatarUrl: "api/File/StoreSpeakerAvatar/{{speakerId}}",
    updateSpeakerUrl: "api/Speaker/UpdateSpeaker",
    updateTalkUrl: "api/Talk/UpdateTalk",
};

export const LABELS = {
    BLOG_URL: "Ссылка на блог",
    CODE_URL: "Ссылка на код",
    COMPANY: "Компания",
    COMPANY_URL: "Сайт компании",
    CONTACTS_URL: "Ссылка на контакты",
    DESCRIPTION: "Описание",
    GIT_HUB_URL: "Ссылка на GitHub",
    HABR_URL: "Ссылка на хабр",
    IDENTITY: "Идентификатор",
    NAME: "Имя",
    SLIDES_URL: "Ссылка на слайды",
    TITLE: "Название",
    TWITTER_URL: "Ссылка на твиттер",
    VIDEO_URL: "Ссылка на видео",
};

export const PATTERNS = {
    IDENTITY: "^[A-Z][\\w-]{2,}$",
    REQUIRED_STRING: "^\\S[\\s\\S]*\\S$",
    URI: "^https?://.+\\..+$",
};

export const MIME_TYPES = {
    AVATAR: "image/png,image/jpeg",
};

export const FILE_SIZES = {
    AVATAR_MAX_SIZE: 40000,
};
