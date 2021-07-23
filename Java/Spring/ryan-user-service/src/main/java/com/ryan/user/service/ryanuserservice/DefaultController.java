package com.ryan.user.service.ryanuserservice;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class DefaultController {
    @GetMapping("/")
    public String index() {
        return "Welcome to Ryan's User Service!\n";
    }

}
