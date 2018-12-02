export const API_ENDPOINTS = {
    addSpeakerUrl: "api/Speaker/AddSpeaker",
    getSpeakerUrl: "api/Speaker/GetSpeaker/{{speakerId}}",
    getSpeakersUrl: "api/Speaker/GetSpeakers",
    storeSpeakerAvatarUrl: "api/File/StoreSpeakerAvatar/{{speakerId}}",
    updateSpeakerUrl: "api/Speaker/UpdateSpeaker",
};

export const LABELS = {
    BLOG_URL: "Ссылка на блог",
    COMPANY: "Компания",
    COMPANY_URL: "Сайт компании",
    CONTACTS_URL: "Ссылка на контакты",
    DESCRIPTION: "Описание",
    GIT_HUB_URL: "Ссылка на GitHub",
    HABR_URL: "Ссылка на хабр",
    IDENTITY: "Идентификатор",
    NAME: "Имя",
    TWITTER_URL: "Ссылка на твиттер",
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
    AVATAR_MAX_SIZE: 75000,
};
