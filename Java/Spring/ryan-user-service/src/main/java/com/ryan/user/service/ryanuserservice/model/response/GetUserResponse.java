package com.ryan.user.service.ryanuserservice.model.response;

import com.ryan.user.service.ryanuserservice.datastore.document.User;

public class GetUserResponse extends BaseResponse {
    private User user;

    public GetUserResponse(User user) {
        this.success = true;
        this.message = "";
        this.user = user;
    }

    public GetUserResponse(User user, String message) {
        this.success = true;
        this.message = message;
        this.user = user;
    }

    public User getUser() {
        return user;
    }
}
