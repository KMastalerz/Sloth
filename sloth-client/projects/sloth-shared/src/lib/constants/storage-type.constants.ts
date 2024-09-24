  // Storage type options
  export const enum StorageType {
    COOKIE,
    SESSION,
    LOCAL
  }

  export class CookieKeys {
    static readonly AuthToken = 'AuthToken';
    static readonly RefreshToken = 'RefreshToken';
    static readonly ExpiresAt = 'ExpiresAt';
    static readonly RefreshExpiresAt = 'RefreshExpiresAt';
    static readonly UserGroup = 'UserGroup';
    static readonly UserRole = 'UserRole';
    static readonly UserName = 'UserName';
  }