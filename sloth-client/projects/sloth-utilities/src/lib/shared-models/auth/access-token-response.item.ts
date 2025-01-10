import { User } from "./user.item";

export interface AccessTokenResponse {
    tokenType: string;
    accessToken: string;
    refreshToken: string;
    expiresAt: Date;
    refreshExpiresAt: Date; 
    user: User;   
}