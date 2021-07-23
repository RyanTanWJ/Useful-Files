package com.ryan.user.service.ryanuserservice;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HealthController {
    @GetMapping("/health")
    public String index() {
        return "I am healthy!\n";
    }

}
