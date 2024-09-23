export interface AccessTokenResponse {
    tokenType: string;
    accessToken: string;
    refreshToken: string;
    expiresAt: Date; 
    refreshExpiresAt: Date;
    user: AccessUser;
}

export interface AccessUser {
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    previousKey?: string; 
    currentKey?: string;  
    userRole: string;
    userGroup: string;
}