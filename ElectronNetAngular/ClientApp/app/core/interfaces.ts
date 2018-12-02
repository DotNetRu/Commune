export interface IMessage {
    duration: number;
    severity: "info" | "warn" | "error";
    text: string;
}
