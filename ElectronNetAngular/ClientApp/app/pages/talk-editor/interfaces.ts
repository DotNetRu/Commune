export interface ISpeakerReference {
    speakerId: string;
}

export interface ITalk {
    id: string;
    speakerIds: ISpeakerReference[];
    title: string;
    description: string;
    codeUrl?: string;
    slidesUrl?: string;
    videoUrl?: string;
}
