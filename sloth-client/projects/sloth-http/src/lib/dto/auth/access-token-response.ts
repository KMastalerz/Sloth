export interface AccessTokenResponse {
    tokenType: string;
    accessToken: string;
    refreshToken: string;
    expiresAt: Date;
    refreshExpiresAt: Date;    
}