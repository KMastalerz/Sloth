import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = "CfDJ8O8IacqVoftHkBu-cS-u6S1smaWlf-929rDxgDLipceLvy1M7aeiZewqDj3rWL06PooVBNB5x0AhYiH44iafXc5FDfn_cM_07CqYyioeg9i-CS_0QPVY9oinpYhoMAWyvbdREkA_0_93OwfhOhdAJHJxkJu7Q1Onzq_JGaVZjhn0F7cYALBE1vF-5xCKiLdtzHEmhPBIDMouD6qWKnl4Adki5vjriN-UU8UST2KerJZHhRoIw4W-Vl-2JZ1Zq784WOzg7WuLAaLIAr5m2XQv6TG09u9FlPmEXcdnTe02CdvNpk3KA6vVurqOqkWJZwaKr-YuX2uwuiekMuurDTzqOJWJ3xMZLYGVaGHfbRQ9ll_ekCsg7cDsdQSeeMLhiyTnqEh56zivxKItuPL0wntJAssOHC0LZs6O7WP1qbUiR8eGLDlMC8mLa-RuhJlB5Il2NjCFVJopB-SNWUjpBm_CUtBg5ZMfVJ7ePZQywcuCTuCf6z_YD6dPIAv3ErgR62FEjyEW8-jZ8Uau99Ksqtp2nhzFBPyGENZmF8jhXhKzsK4r_Di2JYd4XuBRsxlBT_beY-esLYh-88BGdLgk_i7iF55IyHG6h1DfnWpbgFFrCtxb3U9VsnbxeEJx9Q4RbOr2vw0JcIYA-QnHVG3j3VPTcyiolWlsrD7woXwRYgdJD0ZE1e8Sci_7q20v0NKTGd1lokG4WFXIW3vcGulm2ishFmqG3IQA6houxWla93UFRFaofgN-aM1FMAuYYih7rZ5l7VfA5V7nyR-YBWTjY8hBZ3YXHt58fearQ_aRxjl7rMAMtj0-xRAK9RTA1JKSjzL4BJLQ5X3dSLPV8_hfiN-BPpc"
  // Clone the request and add the Authorization header
  const clonedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  // Forward the cloned request instead of the original one
  return next(clonedRequest);
};
